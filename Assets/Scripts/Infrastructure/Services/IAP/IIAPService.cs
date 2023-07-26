using System.Collections.Generic;
using Unity.Plastic.Antlr3.Runtime.Misc;
using UnityEngine.Purchasing;

namespace Infrastructure.Services.IAP
{
    public interface IIAPService : IService
    {
        bool IsInitialized { get; }
        event Action Initialized;
        void Initialize();
        void StartPurchase(string productId);
        PurchaseProcessingResult ProcessPurchase(Product purchasedProduct);
        List<ProductDescription> Products();
    }
}