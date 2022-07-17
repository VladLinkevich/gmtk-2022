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
    private readonly BoardFacade _boardFacade;
    public event Action<Type> ChangeState;

    public PlayerRoll(
      IDiceMover diceMover,
      IDiceRoller diceRoller,
      IPlayerHandler playerHandler,
      IPlayerDiceHandler playerDice,
      BoardFacade boardFacade)
    {
      _diceMover = diceMover;
      _diceRoller = diceRoller;
      _playerHandler = playerHandler;
      _playerDice = playerDice;
      _boardFacade = boardFacade;
    }

    public async void Enter()
    {
      await _diceMover.ToBoard(_playerDice.PlayerDice);
      await _diceRoller.Role();

      Subscribe();
    }

    public void Exit()
    {
    }

    private async void Reroll()
    {
      Unsubscribe();
      await _diceRoller.Role();
      Subscribe();
    }

    private async void Done()
    {
      Unsubscribe();
      await _diceMover.ToCard(_playerDice.PlayerDice);
      ChangeState?.Invoke(typeof(PlayerPick));
    }

    private void Subscribe()
    {
      IgnoreClickObserver(false);
      _boardFacade.Reroll.Observer.Click += Reroll;
      _boardFacade.Done.Observer.Click += Done;
    }

    private void Unsubscribe()
    {
      IgnoreClickObserver(true);
      _boardFacade.Reroll.Observer.Click -= Reroll;
      _boardFacade.Done.Observer.Click -= Done;
    }

    private void IgnoreClickObserver(bool flag)
    {
      foreach (DiceFacade die in _playerDice.PlayerDice)
        die.Observe.Ignore = flag;
    }
  }
}