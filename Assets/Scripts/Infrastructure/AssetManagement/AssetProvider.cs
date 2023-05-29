using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject InstantiateResourse(string path, Vector3 at)
        {
            var prefab = GetPrefabFromResources(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }

        public GameObject InstantiateResourse(string path)
        {
            var prefab = GetPrefabFromResources(path);
            return Object.Instantiate(prefab);
        }

        private static GameObject GetPrefabFromResources(string path)
        {
            return Resources.Load<GameObject>(path);
        }
    }
}