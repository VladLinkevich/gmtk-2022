using System;
using Code.Facade;
using Code.StateMachine;

namespace Code.Game.CardLogic
{
  public class CardActive : IState
  {
    public event Action<Type> ChangeState;
    
    private CardFacade _facade;

    public CardActive()
    {
      
    }

    public void Enter()
    {

    }

    public void Exit()
    {

    }
  }
}