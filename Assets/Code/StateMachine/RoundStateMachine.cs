using System;
using System.Collections.Generic;
using Code.Game;

namespace Code.StateMachine
{
  public interface IStateMachine
  {
    void ChangeState(Type type);
  }

  public class RoundStateMachine : IStateMachine
  {
    private readonly Dictionary<Type, IState> _states;
    private IState _currentState;

    public RoundStateMachine(
      LevelLoader loader,
      PreviewState preview,
      EnemyRound enemyRound,
      PlayerRoll playerRoll,
      PlayerPick playerPick,
      RoundEndAction roundEndAction,
      WinState winState)
    {
      _states = new Dictionary<Type, IState>()
      {
        {typeof(LevelLoader), loader},
        {typeof(PreviewState), preview},
        {typeof(EnemyRound), enemyRound},
        {typeof(PlayerRoll), playerRoll},
        {typeof(PlayerPick), playerPick},
        {typeof(RoundEndAction), roundEndAction},
        {typeof(WinState), winState},
      };
    }

    public void ChangeState(Type type)
    {
      ExitToCurrentState();
      EnterToState(type);
    }

    private void EnterToState(Type type)
    {
      _currentState = _states[type];
      _currentState.ChangeState += ChangeState;
      _currentState.Enter();
    }

    private void ExitToCurrentState()
    {
      if (_currentState == null) 
        return;
      
      _currentState.ChangeState -= ChangeState;
      _currentState.Exit();
    }
  }
}