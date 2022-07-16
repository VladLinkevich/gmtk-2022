using Code.Services.SceneLoadService;
using Zenject;

namespace Code.InitialScene
{
    public class Bootstrapper : IInitializable
    {
        private readonly ISceneLoader _sceneLoader;

        private Bootstrapper(
            ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void Initialize()
        {
            LoadLevelScene();
        }

        private void LoadLevelScene() =>
            _sceneLoader.Load(name: "Game");
    }
}