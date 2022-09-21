using System;
using Code.Game;
using Code.Game.CardLogic;

namespace Code.StateMachine
{
  public class EnemyRound : IState
  {
    public event Action<Type> ChangeState;
    
    private readonly IDiceMover _diceMover;
    private readonly IEnemyDeck _enemy;
    private readonly IDiceRoller _diceRoller;
    private readonly IPickTarget _pickTarget;

    public EnemyRound(
      IDiceMover diceMover,
      ICardPositioner cardPositioner,
      IEnemyDeck enemy,
      IDiceRoller diceRoller,
      IPickTarget pickTarget)
    {
      _diceMover = diceMover;
      _enemy = enemy;
      _diceRoller = diceRoller;
      _pickTarget = pickTarget;
    }

    public async void Enter()
    {
      await _diceMover.ToBoard(_enemy.Card);
      await _diceRoller.Role();
      await _diceMover.ToCard(_enemy.Card);
      await _pickTarget.SelectTarget(_enemy.Card);

      ChangeState?.Invoke(typeof(PlayerRoll));
    }

    public void Exit()
    {
      
    }
  }
}