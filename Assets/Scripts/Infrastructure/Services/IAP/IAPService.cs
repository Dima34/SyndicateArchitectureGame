using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Data;
using Infrastructure.Services.PersistantProgress;
using Unity.Services.Core;
using UnityEditor.U2D;
using UnityEngine.Purchasing;
using Action = Unity.Plastic.Antlr3.Runtime.Misc.Action;

namespace Infrastructure.Services.IAP
{
    public class IAPService : IIAPService
    {
        private readonly IAPProvider _iapProvider;
        private readonly IPersistantProgressService _progerssService;

        public bool  IsInitialized => _iapProvider.IsInitialized;
        public event Action Initialized;

        public IAPService(IAPProvider iapProvider, IPersistantProgressService progerssService)
        {
            _iapProvider = iapProvider;
            _progerssService = progerssService;
        }

        public void Initialize()
        {
            _iapProvider.Initialize(ProcessPurchase);
            _iapProvider.Initialized += () => Initialized?.Invoke();
        }

        public void StartPurchase(string productId) =>
            _iapProvider.StartPurchase(productId);

        public List<ProductDescription> Products() =>
            ProductDescriptions().ToList();

        private IEnumerable<ProductDescription> ProductDescriptions()
        {
            PurchaseData purchaseData = _progerssService.Progress.PurchaseData;

            foreach (string productId in _iapProvider.Products.Keys)
            {
                ProductConfig config = _iapProvider.ProductConfigs[productId];
                Product product = _iapProvider.Products[productId];

                BoughtIAP boughtIAP = purchaseData.BoughtIaps.Find(x => x.IAPId == productId);

                if (ProductBougthOut(boughtIAP, config))
                    continue;

                yield return new ProductDescription
                {
                    Id = productId,
                    Config = config,
                    Product = product,
                    AvailablePurchasesLeft = boughtIAP != null
                        ? config.MaxPurchaseCount - boughtIAP.Count
                        : config.MaxPurchaseCount,
                };
                
            }

            bool ProductBougthOut(BoughtIAP boughtIAP, ProductConfig config) =>
                boughtIAP != null && boughtIAP.Count >= config.MaxPurchaseCount;
        }

        public PurchaseProcessingResult ProcessPurchase(Product purchasedProduct)
        {
            ProductConfig productConfig = _iapProvider.ProductConfigs[purchasedProduct.definition.id];

            switch (productConfig.ItemType)
            {
                case ItemType.Skulls:
                    _progerssService.Progress.CollectedPointsData.Add(productConfig.Quantity);
                    _progerssService.Progress.PurchaseData.AddPurchase(purchasedProduct.definition.id);
                    break;
                default:
                    throw new Exception($"Unknown ItemType {productConfig.ItemType}");
            }

            return PurchaseProcessingResult.Complete;
        }
    }
}