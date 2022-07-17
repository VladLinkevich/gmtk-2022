using System;
using Code.Data;
using Code.Facade;
using Code.Game;

namespace Code.StateMachine
{
  public class PlayerPick : IState
  {
    private readonly IPlayerHandler _playerHandler;
    
    public event Action<Type> ChangeState;

    public PlayerPick(
      IPlayerHandler playerHandler)
    {
      _playerHandler = playerHandler;
    }

    public void Enter()
    {
      foreach (CardFacade card in _playerHandler.Card)
      {
        if (((SideAction) card.DiceFacade.Current.Type & SideAction.Attack) == SideAction.Attack)
        {
          card.MouseObserver.Ignore = false;
          card.Down += PickCard;
        }
      }
    }

    public void Exit()
    {

    }

    private void PickCard(CardFacade card)
    {
      card.Down -= PickCard;

      
    }
  }
}