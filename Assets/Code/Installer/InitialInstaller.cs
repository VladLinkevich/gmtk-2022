using Code.InitialScene;
using Zenject;

namespace Code.Installer
{
    public class InitialInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindBootstrapper();
        }

        private void BindBootstrapper() => 
            Container
                .Bind<IInitializable>()
                .To<Bootstrapper>()
                .AsSingle()
                .NonLazy();
    }
}