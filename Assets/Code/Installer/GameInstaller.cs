using Code.Game;
using Zenject;

namespace Code.Installer
{
  public class GameInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      BindLevelLoader();
      BindFactory();
      BindHandlers();
    }

    private void BindFactory() =>
      Container
        .BindInterfacesTo<CardFactory>()
        .AsSingle();

    private void BindLevelLoader() => 
      Container
        .BindInterfacesTo<LevelLoader>()
        .AsSingle()
        .NonLazy();

    private void BindHandlers() =>
      Container
        .Bind<CardPositioner>()
        .AsSingle()
        .NonLazy();
  }
}