using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.AssetManagement;
using Infrastructure.Services.PersistantProgress;
using Infrastructure.Services.StaticData;
using Infrastructure.States;
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
        private IGameProcessService _gameProcessService;

        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticData,
            IPersistantProgressService progerssService, IAdsService adsService, IGameProcessService gameProcessService)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
            _progerssService = progerssService;
            _adsService = adsService;
            _gameProcessService = gameProcessService;
        }
        
        public async Task CreatUIRoot()
        {
            GameObject gameObject = await _assetProvider.Load<GameObject>(AssetPath.UIROOT);
            _uiRoot = Object.Instantiate(gameObject).transform;
        }

        public void CreateShop()
        {
            var config = _staticData.ForWindow(WindowID.Shop);
            ShopWindow window = Object.Instantiate(config.Prefab, _uiRoot) as ShopWindow;
            
            window.Construct(_adsService,_progerssService, _gameProcessService);
        }

        public void CreateUIConsole() =>
            _assetProvider.InstantiateResourse(AssetPath.INGAMECONSOLE);
    }
}