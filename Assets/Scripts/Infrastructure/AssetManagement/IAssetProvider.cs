using System.Threading.Tasks;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject InstantiateResourse(string path);
        Task<T> Load<T>(AssetReference assetReference) where T : class;
        Task<T> Load<T>(string assetPath) where T : class;
        void CleanUp();
        void Initialize();
        Task<GameObject> Instantiate(string adress);
        Task<GameObject> InstantiateInParent(string adress, Transform parent);
    }
}