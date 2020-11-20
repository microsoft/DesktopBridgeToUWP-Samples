using System;
using System.Windows;

// The following namespace is required for BackgroundTaskBuilder APIs.
using Windows.ApplicationModel.Background;

// The following namespace is required for Registry APIs.
using Microsoft.Win32;

// The following namespace is requires to use the COM registration API (RegisterTypeForComClients).
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace BackgroundTaskWinMainComSample_CS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// This method sets registry keys such that COM understands to launch
        /// the specified executable with parameters when no such process is
        /// running (to start the background task execution).
        /// 
        /// This method must be called on installation of the sparse signed
        /// package. The DesktopBridge application populates this registry key
        /// automatically upon package deployment. These COM server properties
        /// are defined in the package manifest.
        ///
        /// The process that is responsible for handling a particular background
        /// task must call RegisterProcessForBackgroundTask on the IBackgroundTask
        /// derived class. So long as this process is registered with the
        /// aforementioned API, it will be the process that has instances of the
        /// background task invoked.
        /// </summary>
        static void PopulateComRegistrationKeys(string ExecutablePath, Guid TaskClsid)
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey("SOFTWARE", true);
            regKey = regKey.OpenSubKey("Classes", true);
            regKey = regKey.OpenSubKey("CLSID", true);

            regKey = regKey.CreateSubKey(TaskClsid.ToString("B").ToUpper(), true);
            regKey.SetValue(null, System.IO.Path.GetFileName(ExecutablePath));

            regKey = regKey.CreateSubKey("LocalServer32", true);
            regKey.SetValue(null, ExecutablePath);

            regKey.Close();

            return;
        }

        /// <summary>
        /// This method registers the specified Trigger (for example a 15 minute
        /// timer) to start executing some app code identified by a LocalServer
        /// COM entry point.
        /// 
        /// Note that the LocalServer task entry point must be defined with COM
        /// before registering the entry point with BackgroundTaskBuilder.
        /// 
        /// This method returns either the already registered backgroudn task or
        /// a newly created background task with the specified task name.
        /// 
        /// The BackgroundTaskBuilder may throw errors when invalid parameters
        /// are passed in or the registration failed in the system for some
        /// reason.
        /// </summary>
        public IBackgroundTaskRegistration RegisterBackgroundTaskWithSystem(IBackgroundTrigger trigger, Guid entryPointClsid, string taskName)
        {
            foreach (var registrationIterator in BackgroundTaskRegistration.AllTasks)
            {
                if (registrationIterator.Value.Name == taskName)
                {
                    return registrationIterator.Value;
                }
            }

            BackgroundTaskBuilder builder = new BackgroundTaskBuilder();

            builder.SetTrigger(trigger);
            builder.SetTaskEntryPointClsid(entryPointClsid);
            builder.Name = taskName;

            BackgroundTaskRegistration registration;
            try
            {
                registration = builder.Register();
            }
            catch (Exception)
            {
                registration = null;
            }

            return registration;
        }

        /// <summary>
        /// This method unregisters all background tasks belonging to this
        /// application.
        /// </summary>
        public void UnregisterAllBackgroundTasksFromSystem(bool cancelRunningTasks)
        {
            IBackgroundTaskRegistration registration;
            foreach (var registrationIterator in BackgroundTaskRegistration.AllTasks)
            {
                registration = registrationIterator.Value;
                registration.Unregister(cancelRunningTasks);
            }
        }

        /// <summary>
        /// This method register this process as the COM server for the specified
        /// background task class until this process exits or is terminated.
        ///
        /// The process that is responsible for handling a particular background
        /// task must call RegisterTypeForComClients on the IBackgroundTask
        /// derived class. So long as this process is registered with the
        /// aforementioned API, it will be the process that has instances of the
        /// background task invoked.
        /// </summary>
        static void RegisterProcessForBackgroundTask(Type backgroundTaskClass)
        {
            RegistrationServices registrationServices = new RegistrationServices();
            registrationServices.RegisterTypeForComClients(backgroundTaskClass,
                                                           RegistrationClassContext.LocalServer,
                                                           RegistrationConnectionType.MultipleUse);
        }

        public MainWindow()
        {
            InitializeComponent();

            RegisterBackgroundTaskWithSystem(new TimeTrigger(15, false), typeof(TimeTriggeredTask).GUID, typeof(TimeTriggeredTask).Name);
            RegisterProcessForBackgroundTask(typeof(TimeTriggeredTask));
        }
    }
}
