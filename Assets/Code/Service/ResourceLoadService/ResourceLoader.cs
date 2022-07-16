using UnityEngine;

namespace Code.Services.ResourceLoadService
{
    public class ResourceLoader : IResourceLoader
    {
        public GameObject Load(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return UnityEngine.Object.Instantiate(prefab);
        }
        
        public GameObject Load(string path, Transform parent)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return UnityEngine.Object.Instantiate(prefab, parent);
        }

        public GameObject Load(string path, Vector3 at, Transform parent)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return UnityEngine.Object.Instantiate(prefab, at, Quaternion.identity, parent);
        }

        public GameObject Load(string path, Vector3 at, Vector3 rotation, Transform parent)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return UnityEngine.Object.Instantiate(prefab, at, Quaternion.Euler(rotation), parent);
        }
    }
}