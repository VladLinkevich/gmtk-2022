using Code.Services.CoroutineRunnerService;
using Code.Services.ResourceLoadService;
using Code.Services.SceneLoadService;
using Code.UI;
using Sirenix.OdinInspector;
using Zenject;

namespace Code.Installer
{
    public class ProjectInstaller : MonoInstaller
    {
        [ChildGameObjectsOnly] public CoroutineRunner CoroutineRunner;

        public override void InstallBindings()
        {
            BindGameObject();
            BindLevelLoader();
            BindResourceLoader();
            BindUIRootHandler();
            BindUI();
        }

        private void BindUIRootHandler() =>
            Container
                .Bind<IUIRootHandler>()
                .To<UIRootHandler>()
                .AsSingle();

        private void BindResourceLoader() =>
            Container
                .Bind<IResourceLoader>()
                .To<ResourceLoader>()
                .AsSingle();

        private void BindLevelLoader() =>
            Container
                .Bind<ISceneLoader>()
                .To<SceneLoader>()
                .AsSingle();

        private void BindGameObject()
        {
            Container
                .Bind<ICoroutineRunner>()
                .FromInstance(CoroutineRunner)
                .AsSingle();
        }
        
        private void BindUI()
        {
            Container
                .Bind<IUIFactory>()
                .To<UIFactory>()
                .AsSingle(); ;
        }
    }
}