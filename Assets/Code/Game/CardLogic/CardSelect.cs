using System;
using Code.Facade;
using Code.StateMachine;

namespace Code.Game.CardLogic
{
  public class CardSelect : IState
  {
    public event Action<Type> ChangeState;
    
    private CardFacade _facade;

    public CardSelect()
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