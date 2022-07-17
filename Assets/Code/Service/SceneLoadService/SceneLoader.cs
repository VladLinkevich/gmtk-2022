using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Services.SceneLoadService
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ZenjectSceneLoader _sceneLoader;

        public SceneLoader(ZenjectSceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void Load(string name, Action onComplete = null)
        {
            AsyncOperation loader = _sceneLoader.LoadSceneAsync(name);
            loader.completed += operation => Complete(onComplete);
        }

        private void Complete(Action onComplete)
        {
            onComplete?.Invoke();
        }
    }
}