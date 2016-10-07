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

        private async void LaunchDialogOfType(PurchaseDialog.DialogInputType type)
        {
            ErrorDialog errorDlg = null;

            PurchaseDialog dlg = new PurchaseDialog(type, MyPurchases.Consumables);
            dlg.Owner = this;
            if (dlg.ShowDialog() == true)
            {
                var result = await MyPurchases.PurchaseConsumable(dlg.SelectedConsumable);

                switch (result.Status)
                {
                    case StorePurchaseStatus.Succeeded:
                        errorDlg = null;
                        break;

                    case StorePurchaseStatus.NotPurchased:
                        errorDlg = new ErrorDialog("Purchase was not recorded, it may have been canceled.");
                        break;

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
            await MyPurchases.Initialize();
        }
    }
}
