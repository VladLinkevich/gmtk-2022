using UnityEngine;

namespace Code.Services.ResourceLoadService
{
    public interface IResourceLoader
    {
        GameObject Load(string path);
        GameObject Load(string path, Transform parent);
        GameObject Load(string path, Vector3 at, Transform parent);
        GameObject Load(string path, Vector3 at, Vector3 rotation, Transform parent);
    }
}