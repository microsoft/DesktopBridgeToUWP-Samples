using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Foundation;
using Windows.Services.Store;


namespace InAppPurchases
{
    // This interface definition is necessary because this is a non-universal
    // app. This is part of enabling the Store UI purchase flow.
    [ComImport]
    [Guid("3E68D4BD-7135-4D10-8018-9FB6D9F33FA1")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IInitializeWithWindow
    {
        void Initialize(IntPtr hwnd);
    }

    // The StoreConsumableHelper class provides example code of how an application can accept
    // payments using store-managed consumable add-ons.
    public sealed class StoreConsumableHelper : INotifyPropertyChanged
    {
        private static volatile StoreConsumableHelper instance = null;

        static int IAP_E_UNEXPECTED = unchecked((int)0x803f6107);

        // All Store operations are performed on a StoreContext
        private StoreContext storeContext = StoreContext.GetDefault();
        private double purchaseBalance = 0.0;

        public event PropertyChangedEventHandler PropertyChanged;

        // A collection of available store-managed add-ons
        public ObservableCollection<StoreProduct> Consumables { get; set; } = new ObservableCollection<StoreProduct>();

        public double Balance
        {
            get { return purchaseBalance; }
            set
            {
                purchaseBalance = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Balance"));
            }
        }

        private StoreConsumableHelper(){ }

        public static StoreConsumableHelper Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new StoreConsumableHelper();
                }
                return instance;
            }
        }

        public async Task<bool> Initialize()
        {
            // Create a filtered list of the product add-ons that are consumable
            string[] filterList = new string[] { "Consumable" };

            var addOns = await storeContext.GetAssociatedStoreProductsAsync(filterList);

            if(addOns.ExtendedError != null)
            {
                if(addOns.ExtendedError.HResult == IAP_E_UNEXPECTED)
                {
                    return false;
                }
            }

            // Sort the list by price, lowest to highest
            foreach (StoreProduct product in addOns.Products.Values.OrderBy(x=>x.Price.FormattedBasePrice.AsDouble()))
            {
                Consumables.Add(product);
            }

            // Get the current purchased balance from the Store
            Balance = await GetConsumableBalance();
            return true;
        }

        /// <summary>
        /// Retrieve the base price value of unconsumed store-managed consumables.
        /// </summary>
        /// <returns>double</returns>
        public async Task<double> GetConsumableBalance()
        {
            double unconsumedBalance = 0.0;
            foreach (StoreProduct product in Consumables)
            {
                var result = await storeContext.GetConsumableBalanceRemainingAsync(product.StoreId);
                switch (result.Status)
                {
                    case StoreConsumableStatus.Succeeded:
                        unconsumedBalance += result.BalanceRemaining * product.Price.FormattedBasePrice.AsDouble();
                        break;
                    case StoreConsumableStatus.NetworkError:
                        throw new Exception("Network error retrieving consumable balance.");

                    case StoreConsumableStatus.ServerError:
                        throw new Exception("Server error retrieving consumable balance.");

                    default:
                        throw new Exception($"Unknown error retrieving consumable balance for product {product.StoreId}: {product.Title}");
                }
            }

            return unconsumedBalance;
        }

        /// <summary>
        /// Launch the Store UI for purchasing the requested store-managed consumable 
        /// </summary>
        /// <param name="product">The StoreProduct to purchase.</param>
        /// <returns></returns>
        public async Task<StorePurchaseResult> PurchaseConsumable(StoreProduct product)
        {
            // The next two lines are only required because this is a non-universal app
            // so we have to initialize the store context with the main window handle
            // manually. This happens automatically in a Universal app.
            IInitializeWithWindow initWindow = (IInitializeWithWindow)(object)storeContext;
            initWindow.Initialize(System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle);

            var result = await storeContext.RequestPurchaseAsync(product.StoreId);
            if(result.Status == StorePurchaseStatus.Succeeded)
            {
                Balance += product.Price.FormattedBasePrice.AsDouble();
            }
            return result;
        }
    }
}
