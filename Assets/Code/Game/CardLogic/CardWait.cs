using System;
using Code.Facade;
using Code.StateMachine;

namespace Code.Game.CardLogic
{
  public class CardWait : IState
  {
    public event Action<Type> ChangeState;
    
    private CardFacade _facade;

    public CardWait()
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