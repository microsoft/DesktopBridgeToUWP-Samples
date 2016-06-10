using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.ApplicationModel.Background;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MyUWPApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            (App.Current as App).StatusUpdated += MainPage_StatusUpdated;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            colorAnimation.Begin();
            RegisterBackgroundTask("TimerTrigger", new TimeTrigger(15, false));
        }

        private void RegisterBackgroundTask(String triggerName, IBackgroundTrigger trigger)
        {
            // Check if the task is already registered
            foreach (var cur in BackgroundTaskRegistration.AllTasks)
            {
                if (cur.Value.Name == triggerName)
                {
                    // The task is already registered.
                    return;
                }
            }

            BackgroundTaskBuilder builder = new BackgroundTaskBuilder();
            builder.Name = triggerName;
            builder.SetTrigger(trigger);
            builder.TaskEntryPoint = "BackgroundTasks.MyBackgroundTask";
            builder.Register();
        }

        private async void MainPage_StatusUpdated(object sender, string e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                foreach (ComboBoxItem item in comboBox1.Items)
                {
                    if (item.Content.ToString() == e)
                    {
                        comboBox1.SelectedItem = item;
                        break;
                    }
                }
            });
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string value = (comboBox1.SelectedItem as ComboBoxItem).Content.ToString();
            ApplicationData.Current.LocalSettings.Values["Status"] = value;

            // Update the apps live tile
            XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Text03);

            XmlNodeList textNodes = tileXml.GetElementsByTagName("text");
            textNodes[0].InnerText = "MyDesktopApp";
            textNodes[1].InnerText = "Status: ";
            textNodes[2].InnerText = value;
            textNodes[3].InnerText = DateTime.Now.ToString("HH:mm:ss");

            TileNotification tileNotification = new TileNotification(tileXml);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
        }
    }
}
