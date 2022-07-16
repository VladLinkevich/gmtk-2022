using System;

namespace Code.Services.SceneLoadService
{
    public interface ISceneLoader
    {
        void Load(string name, Action onComplete = null);
    }
}