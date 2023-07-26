using UnityEngine.Purchasing;

namespace Infrastructure.Services.IAP
{
    public class ProductDescription
    {
        public Product Product;
        public string Id;
        public ProductConfig Config;
        public int AvailablePurchasesLeft;
    }
}