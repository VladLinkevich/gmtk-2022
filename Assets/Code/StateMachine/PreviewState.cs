using System;
using Code.StateMachine;

namespace Code.Game
{
  public class PreviewState : IState
  {
    public event Action<Type> ChangeState;
    
    public void Enter()
    {
      ChangeState?.Invoke(typeof(EnemyRound));   
    }

    public void Exit()
    {
      
    }
  }
}