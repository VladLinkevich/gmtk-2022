using System;
using Code.StateMachine;

namespace Code.Game.CardLogic
{
  public class CardInactive : IState
  {
    public event Action<Type> ChangeState;

    public CardInactive()
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