using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject InstantiateResourse(string path, Vector3 at);
        GameObject InstantiateResourse(string path);
    }
}