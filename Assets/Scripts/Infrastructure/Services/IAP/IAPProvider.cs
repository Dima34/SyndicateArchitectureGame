using System.Collections.Generic;
using System.Linq;
using Infrastructure.Data;
using Unity.Plastic.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Infrastructure.Services.IAP
{
    public class IAPProvider : IStoreListener
    {
        private IStoreController _controller;
        private IExtensionProvider _extensions;
        public Dictionary<string, ProductConfig> ProductConfigs { get; private set; }
        public Dictionary<string, Product> Products { get; private set; }

        public bool IsInitialized => _controller != null && _extensions != null;

        public Func<Product, PurchaseProcessingResult> _processPurchase;
        public event Action Initialized;

        private const string IAP_CONFIGS_PATH = "IAP/products";

        public void Initialize(Func<Product, PurchaseProcessingResult> processPurchaseFunc)
        {
            ProductConfigs = new Dictionary<string, ProductConfig>();
            Products = new Dictionary<string, Product>();

            _processPurchase = processPurchaseFunc;
            ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            LoadAndFillProductConfigs();
            FillBuilderByProducts(builder);

            UnityPurchasing.Initialize(this, builder);
            Initialized?.Invoke();
        }


        public void LoadAndFillProductConfigs() =>
            ProductConfigs = Resources.Load<TextAsset>(IAP_CONFIGS_PATH)
                .text
                .ToDeserialized<ProductConfigWrapper>()
                .Configs
                .ToDictionary(x => x.Id, x => x);

        private void FillBuilderByProducts(ConfigurationBuilder builder)
        {
            foreach (var productConfig in ProductConfigs.Values)
                builder.AddProduct(productConfig.Id, productConfig.ProductType);
        }

        public void StartPurchase(string productId) =>
            _controller.InitiatePurchase(productId);

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _controller = controller;
            _extensions = extensions;

            foreach (Product product in _controller.products.all)
                Products.Add(product.definition.id, product);

            Debug.Log("Unity purchasing initialized Succesfull!");
        }

        public void OnInitializeFailed(InitializationFailureReason error) =>
            Debug.LogError($"UnityPurchasing OnInitializationFailed {error}");

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            throw new System.NotImplementedException();
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            Debug.Log($"Product purchase success {purchaseEvent.purchasedProduct.definition.id}");

            return _processPurchase(purchaseEvent.purchasedProduct);
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) =>
            Debug.LogError(
                $"UnityPurchasing OnPurchasingError with product {product}; Reason {failureReason}; Transaction id {product.transactionID}");
    }
}