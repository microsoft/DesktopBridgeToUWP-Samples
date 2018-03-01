using System;
using System.Threading.Tasks;
using System.Xml;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace Step4
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        public ProductList Products { get; set; }
        public int MainViewId { get; private set; }
        public MainPage MainPageInstance { get; set; }
        private AppServiceConnection connection = null;
        public AppServiceConnection DatabaseServiceConnection { get { return connection; } }
        private BackgroundTaskDeferral appServiceDeferral = null;
        public event EventHandler AppServiceConnected;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspendingAsync;
        }

        protected override void OnBackgroundActivated(BackgroundActivatedEventArgs args)
        {
            if (args.TaskInstance.TriggerDetails is AppServiceTriggerDetails)
            {
                // Hold deferral reference to make sure AppService connection is always alive
                appServiceDeferral = args.TaskInstance.GetDeferral();
                args.TaskInstance.Canceled += OnTaskCanceled; // Associate a cancellation handler with the background task.

                if (args.TaskInstance.TriggerDetails is AppServiceTriggerDetails details)
                {
                    connection = details.AppServiceConnection;

                    // Notify that connection between app and desktop server has been built
                    AppServiceConnected?.Invoke(this, null);
                }
            }
        }
        private async void OnTaskCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            await SignalConsoleToClose();
            if (this.appServiceDeferral != null)
            {
                // Complete the service deferral.
                this.appServiceDeferral.Complete();
            }
        }

        private async static Task SignalConsoleToClose()
        {
            if ((App.Current as App).DatabaseServiceConnection != null)
            {
                ValueSet valueSet = new ValueSet();
                valueSet.Add("request", "Exit");
                var ignored = await (App.Current as App).DatabaseServiceConnection.SendMessageAsync(valueSet);
            }

        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>

        private async void OnSuspendingAsync(object sender, SuspendingEventArgs e)
        {
            //Save database when user exits
            var main = this.MainPageInstance;
            await main.SaveProductsUsingBridge();

            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();

        }
    }
}
