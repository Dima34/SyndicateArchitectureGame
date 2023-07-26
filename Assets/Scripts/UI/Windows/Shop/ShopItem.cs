using Infrastructure.AssetManagement;
using Infrastructure.Services.IAP;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] private Button _buyButton;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private TextMeshProUGUI _quantityText;
        [SerializeField] private TextMeshProUGUI _availableItemsLeftText;
        [SerializeField] private Image _icon;
        
        private ProductDescription _productDescription;
        private IIAPService _iapService;
        private IAssetProvider _assetsProvider;

        public void Construct(ProductDescription productDescription, IIAPService iapService,
            IAssetProvider assetsProvider)
        {
            _productDescription = productDescription;
            _iapService = iapService;
            _assetsProvider = assetsProvider;
        }

        public async void Initialize()
        {
            _buyButton.onClick.AddListener(OnAddButtonClick);

            _priceText.text = _productDescription.Config.Price;
            _quantityText.text = _productDescription.Config.Quantity.ToString();
            _availableItemsLeftText.text = _productDescription.AvailablePurchasesLeft.ToString();
            _icon.sprite = await _assetsProvider.Load<Sprite>(_productDescription.Config.Icon);
        }

        private void OnAddButtonClick()
        {
            _iapService.StartPurchase(_productDescription.Id);
        }
    }
}