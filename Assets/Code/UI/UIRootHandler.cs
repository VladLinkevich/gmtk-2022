using Code.Services.ResourceLoadService;
using UnityEngine;

namespace Code.UI
{
    public interface IUIRootHandler
    {
        Transform UIRoot { get; }
    }

    public class UIRootHandler : IUIRootHandler
    {
        private readonly IResourceLoader _resourceLoader;
        private readonly Transform _uiRoot;

        public Transform UIRoot => _uiRoot;

        public UIRootHandler(IResourceLoader resourceLoader)
        {
            _resourceLoader = resourceLoader;
            _uiRoot = CreateRoot().transform;
        }

        private GameObject CreateRoot()
        {
            GameObject root = _resourceLoader.Load(Data.Path.Prefab.UI.UIRoot);
            Object.DontDestroyOnLoad(root);
            
            return root;
        }
    }
}