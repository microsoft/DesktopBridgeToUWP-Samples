using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NorthwindDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewOrders : Page
    {
        OrderList orders;
        public ViewOrders()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await LoadOrderHeaders();
            this.DataGrid.ItemsSource = orders;
        }
        private async Task LoadOrderHeaders()
        {
            await LoadOrderHeadersUsingSqlBridge();
        }
        private async Task LoadOrderHeadersUsingSqlBridge()
        {
            orders = new OrderList();
            if ((App.Current as App).DatabaseServiceConnection != null)
            {
                var connString = (App.Current as App).ConnectionString;
                ValueSet valueSet = new ValueSet();
                valueSet.Add("request", "GetOrders");
                valueSet.Add("ConnectionString", connString);

                AppServiceResponse response = await
                    (App.Current as App).DatabaseServiceConnection.SendMessageAsync(valueSet);
                if (response.Status == AppServiceResponseStatus.Success)
                {
                    var json = (string)(response.Message["response"]);
                    if (!string.IsNullOrEmpty(json))
                    {
                        orders.LoadFromJsonString(json);
                    }
                }
                else
                {
                    await new MessageDialog("Error: No response from SqlBridge!").ShowAsync();
                }
            }
            else
            {
                await new MessageDialog("Cannot access SQL Server: SqlBridge is not running!").ShowAsync();
            }
        }
    }
}
