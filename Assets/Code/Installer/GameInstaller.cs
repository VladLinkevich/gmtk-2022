using Code.Data;
using Code.Facade;
using Code.Game;
using Code.Game.CardLogic;
using Code.StateMachine;
using Zenject;

namespace Code.Installer
{
  public class GameInstaller : MonoInstaller
  {
    public BoardFacade BoardFacade;
    
    public override void InstallBindings()
    {
      BindInitializer();

      BindCardDeck();
      BindFactory();
      BindHandlers();
      BindStateMachine();
      BindDice();
      BindEnemyLogic();
      BindBoard();
      BindWinObserver();
      BindInput();
      BindCardDestroyer();
    }

    private void BindCardDeck()
    {
      Container
        .Bind<IEnemyDeck>()
        .To<EnemyDeck>()
        .AsSingle();

      Container
        .Bind<IPlayerDeck>()
        .To<PlayerDeck>()
        .AsSingle();
    }

    private void BindInitializer() =>
      Container
        .Bind<IInitializable>()
        .To<Initialiazer>()
        .AsSingle()
        .NonLazy();

    private void BindCardDestroyer()
    {
      Container
        .Bind<ICardDestroyer>()
        .To<CardDestroyer>()
        .AsSingle();
    }
    
    private void BindWinObserver() =>
      Container
        .Bind<IWinObserver>()
        .To<WinObserver>()
        .AsSingle();

    private void BindEnemyLogic()
    {
      Container
        .Bind<IPickTarget>()
        .To<TargetSelecter>()
        .AsSingle();
      
      Container
        .Bind<IActionWriter>()
        .To<ActionWriter>()
        .AsSingle();
    }

    private void BindFactory()
    {
      Container
        .BindInterfacesTo<CardFactory>()
        .AsSingle();

      Container
        .BindInterfacesTo<ObjectFactory>()
        .AsSingle();
    }

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
        .Bind<IStateMachine>()
        .To<RoundStateMachine>()
        .AsSingle();

      BindState();
    }

    private void BindState()
    {
      Container
        .Bind<EnemyRound>()
        .AsSingle();

      Container
        .Bind<PlayerRoll>()
        .AsSingle();
      
      Container
        .Bind<LevelLoader>()
        .AsSingle();
      
      Container
        .Bind<PreviewState>()
        .AsSingle();

      Container
        .Bind<RoundEndAction>()
        .AsSingle();

      Container
        .Bind<WinState>()
        .AsSingle();

      Container
        .BindInterfacesAndSelfTo<PlayerPick>()
        .AsSingle();
    }

    private void BindBoard() =>
      Container
        .Bind<BoardFacade>()
        .FromInstance(BoardFacade)
        .AsSingle();

    private void BindInput() =>
      Container
        .Bind<Control>()
        .AsSingle();
  }
}