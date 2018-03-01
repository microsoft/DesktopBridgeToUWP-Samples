//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.Payments;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NorthwindDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OrderPage : Page
    {
        DataPackageView packageView;
        bool isCompactOverlay = false;
        TextBlock footerTextBlock;

        public OrderDetailsList OrderDetails { get; set; }
        public string OrderTotal { get { return OrderDetails.GetNetTotal().ToString("######0.00"); } }
        public OrderPage()
        {
            this.InitializeComponent();
            
            // Footer textblock is created programmatically here
            footerTextBlock = new TextBlock();
            footerTextBlock.Margin = new Thickness(16);
            OrderDetailsListView.Footer = footerTextBlock;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            OrderDetails = new OrderDetailsList();
            OrderDetailsListView.ItemsSource = OrderDetails;
            footerTextBlock.Text = "Order total:  " + OrderTotal;
            
            // This window can be launched multiple times so showing the ID here for demo
            var id = ApplicationView.GetForCurrentView().Id;
            ApplicationView.GetForCurrentView().Title = "New Order " + id.ToString();
        }
        #region Drop handling code
        private void OrderText_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Copy;
            packageView = e.DataView;
        }

        private async void OrderText_Drop(object sender, DragEventArgs e)
        {
            // If valid ProductID is passed in, add a line item to collection
            if(e.DataView.Contains(StandardDataFormats.Text))
            {
                var inventoryId = await e.DataView.GetTextAsync();
                int productId = 0;
                int.TryParse(inventoryId, out productId);
                if(productId > 0)
                {
                    var products = (App.Current as App).Products;
                    var product = products.GetProductById(productId);
                    if (product != null)
                    {
                        var lineItem = new OrderDetail();
                        lineItem.ProductID = productId;
                        lineItem.ProductName = product.ProductName;
                        lineItem.Quantity = 1;
                        lineItem.UnitPrice = product.UnitPrice;

                        OrderDetails.Add(lineItem);
                    }
                }
            }
        }
        #endregion

        #region Toggle CompactOverlay (picture in picture) mode
        private async void Toggle_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var view = ApplicationView.GetForCurrentView();
            if (isCompactOverlay)
            {
                await view.TryEnterViewModeAsync(ApplicationViewMode.Default);
            }
            else
            {
                // Setting the initial size isn't required but it's handy
                var compactOptions = ViewModePreferences.CreateDefault(ApplicationViewMode.CompactOverlay);
                compactOptions.CustomSize = new Windows.Foundation.Size(400, 600);
                await view.TryEnterViewModeAsync(ApplicationViewMode.CompactOverlay, compactOptions);
            }
            isCompactOverlay = !isCompactOverlay;
        }
        #endregion

        #region Microsoft Payments code
        private async void Pay_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // Package line items and total to submit to Microsoft Payments
            var paymentItems = new List<PaymentItem>();
            foreach (OrderDetail detail in OrderDetails)
            {
                paymentItems.Add(new PaymentItem(detail.ProductName,
                    new PaymentCurrencyAmount(detail.NetAmountString, "USD")));
            }

            var paymentDetails = new PaymentDetails();
            paymentDetails.Total = new PaymentItem("Total",
                new PaymentCurrencyAmount(OrderTotal, "USD"));
            paymentDetails.DisplayItems = paymentItems;
            paymentDetails.ShippingOptions = CreateShippingOptions();
            var paymentOptions = new PaymentOptions()
            {
                RequestShipping = true,
                ShippingType = PaymentShippingType.Delivery,
                RequestPayerEmail = PaymentOptionPresence.Optional,
                RequestPayerName = PaymentOptionPresence.Required,
                RequestPayerPhoneNumber = PaymentOptionPresence.None
            };
            var merchantInfo = new PaymentMerchantInfo(new Uri("http://www.contoso.com"));
            var paymentMethods = new List<PaymentMethodData>();
            paymentMethods.Add(new PaymentMethodData(new List<string>()
            { "basic-card" }));

            // Send payment request to Microsoft for cardholder authentication
            // Microsoft prompts user to enter CVV
            var paymentRequest = new PaymentRequest(paymentDetails,
                paymentMethods, merchantInfo, paymentOptions);
            var mediator = new PaymentMediator();
            var result = await mediator.SubmitPaymentRequestAsync(paymentRequest);

            await UpdateUI(result);
        }

        private async Task UpdateUI(PaymentRequestSubmitResult result)
        {
            if (result.Status == PaymentRequestStatus.Succeeded)
            {
                // If payment request is successful, Microsoft returns a json token
                //   containing Microsoft Wallet data to submit to merchant's payment processor
                Debug.WriteLine("Method ID: " + result.Response.PaymentToken.PaymentMethodId);
                Debug.WriteLine("Payment JSON Token: " + (result.Response.PaymentToken.JsonDetails ?? ""));

                // Simulate payment processing by merchant's payment processor
                // Note: actual time will vary quite a bit by payment processor
                await Task.Delay(TimeSpan.FromSeconds(2));

                // Report that we successfully charged their card using the payment token we were given
                await result.Response.CompleteAsync(PaymentRequestCompletionStatus.Succeeded);

                await new MessageDialog("Payment succeeded!").ShowAsync();
                Save_Tapped(this, null);
            }
            else
            {
                await new MessageDialog("Payment failed, status: " + result.Status).ShowAsync();
            }
        }

        private IReadOnlyList<PaymentShippingOption> CreateShippingOptions()
        {
            List<PaymentShippingOption> paymentShippingOptions = new List<PaymentShippingOption>();

            PaymentShippingOption shippingOption = new PaymentShippingOption("Standard", new PaymentCurrencyAmount("2.00", "USD"), false, "STANDARD");
            paymentShippingOptions.Add(shippingOption);

            shippingOption = new PaymentShippingOption("Two Day", new PaymentCurrencyAmount("5.99", "USD"), false, "TWODAY");
            paymentShippingOptions.Add(shippingOption);

            return paymentShippingOptions;
        }
#endregion

        private async void Save_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var order = new Order();
            order.OrderDetails = OrderDetails;
            var savedOrderId = await SaveOrderUsingSqlBridge(order);
            if (!string.IsNullOrEmpty(savedOrderId))
            {
                ShowNotification("Order saved: " + savedOrderId);

                // Close this window
                var ordersView = ApplicationView.GetForCurrentView();
                await ordersView.TryConsolidateAsync();
            }
            else
            {
                await new MessageDialog("ERROR! There was a problem saving this order.").ShowAsync();
            }
        }

        private async Task<string> SaveOrderUsingSqlBridge(Order order)
        {
            if ((App.Current as App).DatabaseServiceConnection != null)
            {
                var json = order.ToJson();
                var connString = (App.Current as App).ConnectionString;

                ValueSet valueSet = new ValueSet();
                valueSet.Add("request", "SaveOrder");
                valueSet.Add("OrderJson", json);
                valueSet.Add("ConnectionString", connString);

                AppServiceResponse response = await (App.Current as App).DatabaseServiceConnection.SendMessageAsync(valueSet);
                if (response.Status == AppServiceResponseStatus.Success)
                {
                    var result = (string)(response.Message["response"]);
                    return result;
                }
                else
                {
                    await new MessageDialog("Error! No response from SqlBridge!").ShowAsync();
                }
            }
            else
            {
                await new MessageDialog("Cannot access SQL Server: SqlBridge is not running!").ShowAsync();
            }
            return "";
        }

        private void ShowNotification(string message)
        {
            var notifier = ToastNotificationManager.CreateToastNotifier();
            if (notifier == null)
                return;
            var toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
            var toastNodeList = toastXml.GetElementsByTagName("text");
            toastNodeList.Item(0).AppendChild(toastXml.CreateTextNode("Northwind Traders"));
            toastNodeList.Item(1).AppendChild(toastXml.CreateTextNode(message));
            var toastNode = toastXml.SelectSingleNode("/toast");
            var audio = toastXml.CreateElement("audio");
            audio.SetAttribute("src", "ms-winsoundevent:Notification.SMS");
            ToastNotification toast = new ToastNotification(toastXml);
            notifier.Show(toast);
        }

        private void QuantityTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int quantity = 0;
            int.TryParse((sender as TextBox).Text, out quantity);
            var dc = (sender as TextBox).DataContext;
            var orderDetail = (dc as OrderDetail);
            orderDetail.Quantity = quantity;
            orderDetail.UpdateTotal();

            // Display extended price for line item
            var parent = VisualTreeHelper.GetParent(sender as DependencyObject);
            var childCount = VisualTreeHelper.GetChildrenCount(parent);
            var totalTextbox = (TextBox)VisualTreeHelper.GetChild(parent, childCount - 1);
            totalTextbox.Text = orderDetail.NetAmountString;

            // Display updated order total
            footerTextBlock.Text = "Order total:  " + OrderTotal;
        }


    }
}

