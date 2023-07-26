using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.AssetManagement;
using Infrastructure.Services.IAP;
using Infrastructure.Services.PersistantProgress;
using UnityEngine;

namespace UI.Windows.Shop
{
    public class ShopItemsContainer : MonoBehaviour
    {
        [SerializeField] private GameObject[] _shopUnavailableObjects;
        [SerializeField] private Transform _parent;
        
        private IIAPService _iapService;
        private IPersistantProgressService _persistantProgressService;
        private IAssetProvider _assetProvider;
        private List<ShopItem> _shopItems = new List<ShopItem>(); 
        
        private const string SHOP_ITEM_ADRESS = "ShopItem";


        public void Construct(IIAPService iapService, IPersistantProgressService persistantProgressService,
            IAssetProvider assetProvider)
        {
            _iapService = iapService;
            _persistantProgressService = persistantProgressService;
            _assetProvider = assetProvider;
        }

        public void Initialize() =>
            RefreshAvailableItems();

        public void Subscribe()
        {
            _iapService.Initialized += RefreshAvailableItems;
            _persistantProgressService.Progress.PurchaseData.OnChange += RefreshAvailableItems;
        }

        public void Cleanup()
        {
            _iapService.Initialized -= RefreshAvailableItems;
            _persistantProgressService.Progress.PurchaseData.OnChange -= RefreshAvailableItems;
        }

        private async void RefreshAvailableItems()
        {
            UpdateShopUnavailableObjects();
            
            if(!_iapService.IsInitialized)
                return;
            
            DestroyOldShopItems();
            await CreateShopItems();
        }

        private async Task CreateShopItems()
        {
            foreach (ProductDescription productDescription in _iapService.Products())
            {
                GameObject shopItemObject = await _assetProvider.InstantiateInParent(SHOP_ITEM_ADRESS, _parent);
                ShopItem shopItem = shopItemObject.GetComponent<ShopItem>();

                shopItem.Construct(productDescription, _iapService, _assetProvider);
                shopItem.Initialize();
                
                _shopItems.Add(shopItem);
            }
        }

        private void DestroyOldShopItems()
        {
            foreach (ShopItem item in _shopItems)
                Destroy(item.gameObject);
            
            _shopItems.Clear();
        }

        private void UpdateShopUnavailableObjects()
        {
            foreach (GameObject unavailableObject in _shopUnavailableObjects)
                unavailableObject.SetActive(!_iapService.IsInitialized);
        }
    }
}