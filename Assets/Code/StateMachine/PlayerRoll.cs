using System;
using Code.Facade;
using Code.Game;

namespace Code.StateMachine
{
  public class PlayerRoll : IState
  {
    private readonly IDiceMover _diceMover;
    private readonly IDiceRoller _diceRoller;
    private readonly IPlayerHandler _playerHandler;
    private readonly IPlayerDiceHandler _playerDice;
    public event Action<Type> ChangeState;

    public PlayerRoll(
      IDiceMover diceMover,
      IDiceRoller diceRoller,
      IPlayerHandler playerHandler,
      IPlayerDiceHandler playerDice)
    {
      _diceMover = diceMover;
      _diceRoller = diceRoller;
      _playerHandler = playerHandler;
      _playerDice = playerDice;
    }

    public async void Enter()
    {
      await _diceMover.ToBoard(_playerDice.PlayerDice);
      await _diceRoller.Role();
      IgnoreClickObserver(false);
    }

    public void Exit()
    {
      
    }

    private void IgnoreClickObserver(bool flag)
    {
      foreach (DiceFacade die in _playerDice.PlayerDice)
        die.ObserveDice.Ignore = flag;
    }
  }
}