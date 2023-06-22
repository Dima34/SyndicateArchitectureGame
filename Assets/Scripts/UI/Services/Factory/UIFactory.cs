using Infrastructure;
using Infrastructure.AssetManagement;
using Infrastructure.Services.PersistantProgress;
using Infrastructure.Services.StaticData;
using Infrastructure.States;
using StaticData;
using UI.Services.Windows;
using UI.Windows;
using UnityEngine;

namespace UI.Services.Factory
{
    class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private Transform _uiRoot;
        private readonly IStaticDataService _staticData;
        private readonly IPersistantProgressService _progerssService;
        private IAdsService _adsService;

        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticData, IPersistantProgressService progerssService, IAdsService adsService)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
            _progerssService = progerssService;
            _adsService = adsService;
        }

        public void CreatUIRoot() =>
            _uiRoot = _assetProvider.InstantiateResourse( AssetPath.UIROOT_PATH).transform;

        public void CreateShop()
        {
            var config = _staticData.ForWindow(WindowID.Shop);
            ShopWindow window = Object.Instantiate(config.Prefab, _uiRoot) as ShopWindow;
            
            window.Construct(_adsService,_progerssService);
        }
    }
}