using System;
using Code.Game;

namespace Code.StateMachine
{
  public class RoundEndAction : IState
  {
    private readonly IActionWriter _actionWriter;
    
    public event Action<Type> ChangeState;

    public RoundEndAction(IActionWriter actionWriter)
    {
      _actionWriter = actionWriter;
    }

    public void Enter()
    {
      _actionWriter.Release();
      ChangeState?.Invoke(typeof(EnemyRound));
    }

    public void Exit()
    {

    }
  }
}