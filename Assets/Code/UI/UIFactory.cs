using System;
using Code.Data;
using Code.Services.ResourceLoadService;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Code.UI
{
    public interface IUIFactory
    {
        T Create<T>() where T : class;
        T CreateSubWindow<T>() where T : class;
    }

    public class UIFactory : IUIFactory, IDisposable
    {
        private readonly IResourceLoader _loader;
        private readonly Transform _uiRoot;
        
        private GameObject _currentWindow;

        public UIFactory(
            IResourceLoader loader,
            IUIRootHandler uiRootHandler)
        {
            _loader = loader;
            _uiRoot = uiRootHandler.UIRoot;
        }

        public T Create<T>() where T : class
        {
            CleanUpCurrentWindow();
            _currentWindow = _loader.Load(Path.Prefab.UI.Window + ParsePath<T>(), _uiRoot);
            return _currentWindow.GetComponent<T>();
        }
        
        public T CreateSubWindow<T>() where T : class => 
            _loader
                .Load(Path.Prefab.UI.Window + ParsePath<T>(), _uiRoot)
                .GetComponent<T>();
        
        public void Dispose()
        {
            for (int i = _uiRoot.childCount - 1, end = 0; i >= end; --i) 
                Object.Destroy(_uiRoot.GetChild(i).gameObject);
        }

        private void CleanUpCurrentWindow()
        {
            if (_currentWindow == false)
                return;
            
            Object.Destroy(_currentWindow.gameObject);
            _currentWindow = null;
        }

        private static string ParsePath<T>() where T : class
        {
            string path = typeof(T).ToString();
            path = path.Substring(path.LastIndexOf('.') + 1);
            return path;
        }
    }
}