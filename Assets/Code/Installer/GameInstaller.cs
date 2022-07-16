using Code.Game;
using Code.StateMachine;
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
      BindStateMachine();
      BindDice();
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


    private void BindDice() =>
      Container
        .Bind<IDiceMover>()
        .To<DiceMover>()
        .AsSingle()
        .NonLazy();

    private void BindStateMachine()
    {
      Container
        .Bind<RoundStateMachine>()
        .AsSingle()
        .NonLazy();

      BindState();
    }

    private void BindState()
    {
      Container
        .Bind<EnemyRound>()
        .AsSingle()
        .NonLazy();
    }
  }
}