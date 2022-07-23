using System;
using System.Collections.Generic;
using Code.StateMachine;

namespace Code.Game.CardLogic
{
  public class CardStateMachine
  {
    private readonly ILoadLevel _level;
    private readonly WinState _winState;

    private Dictionary<Type, IState> _states;
    private IState _currentState;

    public CardStateMachine(
      CardSelect cardSelect,
      CardInactive cardInactive,
      CardActive cardActive,
      CardWait cardWait)
    {
      _states = new Dictionary<Type, IState>()
      {
        {typeof(CardSelect), cardSelect},
        {typeof(CardInactive), cardInactive},
        {typeof(CardActive), cardActive},
        {typeof(CardWait), cardWait},
      };
      
      EnterToState(typeof(CardWait));
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