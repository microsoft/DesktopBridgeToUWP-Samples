
using System;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.UI.Core.Preview;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SystrayExtension
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            SystemNavigationManagerPreview mgr =
                SystemNavigationManagerPreview.GetForCurrentView();
            mgr.CloseRequested += SystemNavigationManager_CloseRequested;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            octopusMove.Begin();
        }

        private async void SystemNavigationManager_CloseRequested(object sender, SystemNavigationCloseRequestedPreviewEventArgs e)
        {
            Deferral deferral = e.GetDeferral();
            ConfirmCloseDialog dlg = new ConfirmCloseDialog();
            ContentDialogResult result = await dlg.ShowAsync();
            if (result == ContentDialogResult.Secondary)
            {
                // user cancelled the close operation
                e.Handled = true;
                deferral.Complete();
            }
            else
            {
                switch (dlg.Result)
                {
                    case CloseAction.Terminate:
                        e.Handled = false;
                        deferral.Complete();
                        break;

                    case CloseAction.Systray:
                        if (ApiInformation.IsApiContractPresent(
                             "Windows.ApplicationModel.FullTrustAppContract", 1, 0))
                        {
                            await FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync();
                        }
                        e.Handled = false;
                        deferral.Complete();
                        break;
                }
            }
        }
    }
}
