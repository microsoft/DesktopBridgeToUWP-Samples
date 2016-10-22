using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Windows.Services.Store;
using Windows.System;

namespace InAppPurchases
{
    /// <summary>
    /// Interaction logic for PurchaseWindow.xaml
    /// </summary>
    public partial class PurchaseWindow : Window
    {
        public StoreConsumableHelper MyPurchases { get; set; }
        public PurchaseWindow()
        {
            InitializeComponent();
            // Bind the StoreConsumable helper to the Window so we can 
            // use the StoreProduct into for each consumable in the UI.
            MyPurchases = StoreConsumableHelper.Instance;
            this.DataContext = MyPurchases;
        }

        private void btnButtonOption_Click(object sender, RoutedEventArgs e)
        {
            LaunchDialogOfType(PurchaseDialog.DialogInputType.Button);
        }

        private void btnRadioButtonOption_Click(object sender, RoutedEventArgs e)
        {
            LaunchDialogOfType(PurchaseDialog.DialogInputType.RadioButton);
        }

        private void btnSliderOption_Click(object sender, RoutedEventArgs e)
        {
            LaunchDialogOfType(PurchaseDialog.DialogInputType.Slider);
        }

        /// <summary>
        /// Launch the PurchaseDialog displaying the consumable options. How the options
        /// are displayed is determined by the DialogInputType passed in.
        /// </summary>
        /// <param name="type"></param>
        private async void LaunchDialogOfType(PurchaseDialog.DialogInputType type)
        {
            ErrorDialog errorDlg = null;

            PurchaseDialog dlg = new PurchaseDialog(type, MyPurchases.Consumables);
            dlg.Owner = this;
            if (dlg.ShowDialog().Value)
            {
                var result = await MyPurchases.PurchaseConsumable(dlg.SelectedConsumable);

                switch (result.Status)
                {
                    case StorePurchaseStatus.Succeeded:
                        errorDlg = null;
                        break;

                    // Uncomment this case to handle the scenario where a user cancels the
                    // Store purchase process.
                    //case StorePurchaseStatus.NotPurchased:
                    //    [Code to handle a cancelled transaction]
                    //    break;

                    case StorePurchaseStatus.NetworkError:
                        errorDlg = new ErrorDialog("Purchase was not recorded due to a network error.");
                        break;

                    case StorePurchaseStatus.ServerError:
                        errorDlg = new ErrorDialog("Purchase was not recorded due to a server error.");
                        break;

                    default:
                        errorDlg = new ErrorDialog("Purchase was not recorded due to an unknown error.");
                        break;
                }

                if(errorDlg != null)
                {
                    if (result.ExtendedError != null)
                    {
                        errorDlg.ExtendedMessage = result.ExtendedError.Message;
                    }

                    errorDlg.Owner = this;
                    errorDlg.ShowDialog();
                }
                else
                {
                    ThankYouDialog thankyouDlg = new ThankYouDialog();
                    thankyouDlg.Owner = this;
                    thankyouDlg.ShowDialog();
                }
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(!await MyPurchases.Initialize())
            {
                ErrorDialog errorDlg = new ErrorDialog("Please update the AppXManifest.xml file to include your application identity information. See the README for information on how to do this.");
                errorDlg.Owner = this;
                errorDlg.ShowDialog();
            }
        }
    }
}
