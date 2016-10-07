using System;
using System.Collections.Generic;
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

namespace InAppPurchases
{
    /// <summary>
    /// Interaction logic for ThankYou.xaml
    /// </summary>
    public partial class ErrorDialog : Window
    {
        public String Message { get; set; }
        public String ExtendedMessage { get; set; } = "";
        public ErrorDialog(string errorMessage, string extendedMessage = "")
        {
            InitializeComponent();
            Message = errorMessage;
            ExtendedMessage = extendedMessage;
            DataContext = this;
        }
    }
}
