using _Project.Logic.Infrastructure.Services;
using UnityEngine;

namespace _Project.Logic.Infrastructure.AssetManagement
{
    public interface IAssets : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
    }
}