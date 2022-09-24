using System;
using System.Linq;
using Code.Facade;
using Code.Game;
using Code.Game.CardLogic;
using UnityEngine;

namespace Code.StateMachine
{
  public class PlayerRoll : IState
  {
    private readonly IDiceMover _diceMover;
    private readonly IDiceRoller _diceRoller;
    private readonly IPlayerDeck _player;
    private readonly BoardFacade _boardFacade;
    private static readonly int Show = Animator.StringToHash("show");
    private int _role;
    public event Action<Type> ChangeState;

    public PlayerRoll(
      IDiceMover diceMover,
      IDiceRoller diceRoller,
      IPlayerDeck player,
      BoardFacade boardFacade)
    {
      _diceMover = diceMover;
      _diceRoller = diceRoller;
      _player = player;
      _boardFacade = boardFacade;
    }

    public async void Enter()
    {
      _role = 2;
      RerollLabel();
      _boardFacade.Animator.SetTrigger(Show);
      await _diceMover.ToBoard(_player.Card);
      await _diceRoller.Role();

      Subscribe();
    }

    public void Exit()
    {
    }

    private async void Reroll()
    {
      Unsubscribe();
      
      --_role;
      RerollLabel();
      await _diceRoller.Role();
      if (_role == 0)
        Done();

      Subscribe();
    }

    private async void Done()
    {
      Unsubscribe();
      
      await _diceMover.ToCard(_player.Card);
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
      foreach (DiceFacade die in _player.Card.Select(card => card.DiceFacade))
        die.Observe.Ignore = flag;
    }

    private void RerollLabel() => 
      _boardFacade.Reroll.Label.text = $"Reroll {_role}";
  }
}