using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using Windows.Services.Store;

namespace InAppPurchases
{
    /// <summary>
    /// Interaction logic for PurchaseDialog.xaml
    /// </summary>
    public partial class PurchaseDialog : Window
    {
        private DialogInputType _type;

        // These properties allow the correct interface to display based on the type of 
        // dialog the user requested.
        public Visibility RadioButtonInterfaceVisibility { get { return ConvertDialogTypeToVisibility(DialogInputType.RadioButton); } private set { } }
        public Visibility ButtonInterfaceVisibility { get { return ConvertDialogTypeToVisibility(DialogInputType.Button); } private set { } }
        public Visibility SliderInterfaceVisibility { get { return ConvertDialogTypeToVisibility(DialogInputType.Slider); } private set { } }

        public ObservableCollection<StoreProduct> Options { get; set; }

        public StoreProduct SelectedConsumable { get; set; } = null;

        public PurchaseDialog(DialogInputType type, ObservableCollection<StoreProduct> options)
        {
            if (options == null || options.Count < 3)
                throw new ArgumentNullException("options must contain at least 3 StoreProduct items.");

            InitializeComponent();
            DataContext = this;
            _type = type;
            Options = options;
        }

        private Visibility ConvertDialogTypeToVisibility(DialogInputType type)
        {
            return (_type == type) ? Visibility.Visible : Visibility.Collapsed;
        }

        public enum DialogInputType
        {
            RadioButton,
            Slider,
            Button
        }

        private void btnPurchase_Click(object sender, RoutedEventArgs e)
        {
            switch (_type)
            {
                case DialogInputType.RadioButton:
                    RadioButton selectedRadio = radioButtonGroup.Children.OfType<RadioButton>().FirstOrDefault(r => r.IsChecked == true);
                    SelectedConsumable = selectedRadio.Tag as StoreProduct;
                    break;
                case DialogInputType.Button:
                    RadioButton selectedButton = buttonGroup.Children.OfType<RadioButton>().FirstOrDefault(r => r.IsChecked == true);
                    SelectedConsumable = selectedButton.Tag as StoreProduct;
                    break;
                case DialogInputType.Slider:
                    SelectedConsumable = Options[(int)sliderCtrl.Value];
                    break;
                default:
                    break;
            }

            this.DialogResult = (SelectedConsumable != null) ? true : false;
        }
    }
}
