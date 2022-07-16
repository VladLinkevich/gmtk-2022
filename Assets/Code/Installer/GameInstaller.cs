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
    }

    private void BindFactory() =>
      Container
        .Bind<ICardFactory>()
        .To<CardFactory>()
        .AsSingle()
        .NonLazy();

    private void BindLevelLoader() => 
      Container
        .Bind<LevelLoader>()
        .AsSingle()
        .NonLazy();
  }
}