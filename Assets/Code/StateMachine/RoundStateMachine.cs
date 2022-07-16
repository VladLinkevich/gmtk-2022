using System;
using System.Collections.Generic;
using Code.Game;

namespace Code.StateMachine
{
  public class RoundStateMachine
  {
    private readonly ILoadLevel _level;
    
    private Dictionary<Type, IState> _states;
    private IState _currentState;

    public RoundStateMachine(
      ILoadLevel level,
      EnemyRound enemyRound)
    {
      _level = level;
      _states = new Dictionary<Type, IState>()
      {
        {typeof(EnemyRound), enemyRound},
      };

      _level.Complete += Initialize;
    }

    private void Initialize()
    {
      _level.Complete -= Initialize;
      EnterToState(typeof(EnemyRound));
    }

    private void ChangeState(Type type)
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