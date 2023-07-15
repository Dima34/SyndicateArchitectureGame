using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _compleatedCache = new Dictionary<string, AsyncOperationHandle>();
        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new Dictionary<string, List<AsyncOperationHandle>>();

        public void Initialize()
        {
            Addressables.InitializeAsync();
        }

        public async Task<T> Load<T>(AssetReference assetReference) where T : class
        {
            if (_compleatedCache.TryGetValue(assetReference.AssetGUID, out AsyncOperationHandle compleatedHandle))
                return compleatedHandle.Result as T;
            
            AsyncOperationHandle<T> handle =
                Addressables.LoadAssetAsync<T>(assetReference);

            AddHandleToCacheWhenComplete(handle, assetReference.AssetGUID);

            return await handle.Task;
        }

        public async Task<T> Load<T>(string assetPath) where T : class
        {
            if (_compleatedCache.TryGetValue(assetPath, out AsyncOperationHandle compleatedHandle))
                return compleatedHandle.Result as T;
            
            AsyncOperationHandle<T> handle =
                Addressables.LoadAssetAsync<T>(assetPath);

            AddHandleToCacheWhenComplete(handle,assetPath);

            return await handle.Task;
        }

        private void AddHandleToCacheWhenComplete<T>(AsyncOperationHandle<T> handle, string cacheKey) where T : class
        {
            handle.Completed += h =>
                _compleatedCache[cacheKey] = h;

            AddHandle(handle, cacheKey);
        }

        private void AddHandle<T>(AsyncOperationHandle<T> handle, string key) where T : class
        {
            if (!_handles.TryGetValue(key, out List<AsyncOperationHandle> resourceHandle))
            {
                resourceHandle = new List<AsyncOperationHandle>();
                _handles[key] = resourceHandle;
            }

            resourceHandle.Add(handle);
        }

        public void CleanUp()
        {
            foreach (List<AsyncOperationHandle> resourceHandles in _handles.Values)
                foreach (AsyncOperationHandle handle in resourceHandles)
                    Addressables.Release(handle);
            
            _compleatedCache.Clear();
            _handles.Clear();
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