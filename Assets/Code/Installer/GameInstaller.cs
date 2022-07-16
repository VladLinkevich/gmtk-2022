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
      BindEnemyLogic();
    }

    private void BindEnemyLogic() =>
      Container
        .Bind<IPickTarget>()
        .To<PickTarget>()
        .AsSingle();

    private void BindFactory()
    {
      Container
        .BindInterfacesTo<CardFactory>()
        .AsSingle();

      Container
        .BindInterfacesTo<ObjectFactory>()
        .AsSingle();
    }

    private void BindLevelLoader() => 
      Container
        .BindInterfacesTo<LevelLoader>()
        .AsSingle()
        .NonLazy();

    private void BindHandlers() =>
      Container
        .Bind<ICardPositioner>()
        .To<CardPositioner>()
        .AsSingle();


    private void BindDice()
    {
      Container
        .Bind<IDiceMover>()
        .To<DiceMover>()
        .AsSingle();
      
      Container
        .Bind<IDiceRoller>()
        .To<DiceRoller>()
        .AsSingle();
    }

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