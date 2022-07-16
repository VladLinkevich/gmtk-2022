using System;

namespace Code.StateMachine
{
  public interface IState
  {
    event Action<Type> ChangeState;
    void Enter();
    void Exit();
  }
}