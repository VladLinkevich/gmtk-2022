using Code.Facade;
using Code.Game.CardLogic;
using Zenject;

public class CardInstaller : MonoInstaller
{
  public CardFacade CardFacade;
  public MouseObserver MouseObserver;
  
  public override void InstallBindings()
  {
    BindCard();
    BindGameObject();
    CardStateMachine();
  }

  private void BindCard() =>
    Container
      .Bind<Card>()
      .AsSingle();

  private void CardStateMachine()
  {
    Container
      .Bind<CardStateMachine>()
      .AsSingle();

    CardState();
  }

  private void CardState()
  {
    Container
      .Bind<CardSelect>()
      .AsSingle();

    Container
      .Bind<CardWait>()
      .AsSingle();
  }

  private void BindGameObject()
  {
    Container
      .Bind<CardFacade>()
      .FromInstance(CardFacade)
      .AsSingle();
    
    Container
      .Bind<MouseObserver>()
      .FromInstance(MouseObserver)
      .AsSingle();
  }
}