using System;
using Code.Game;
using Code.Game.CardLogic;

namespace Code.StateMachine
{
  public class EnemyRound : IState
  {
    public event Action<Type> ChangeState;
    
    private readonly IDiceMover _diceMover;
    private readonly IEnemyHandler _enemyHandler;
    private readonly IEnemyDiceHandler _enemyDice;
    private readonly IDiceRoller _diceRoller;
    private readonly IPickTarget _pickTarget;

    public EnemyRound(
      IDiceMover diceMover,
      ICardPositioner cardPositioner,
      IEnemyHandler enemyHandler,
      IEnemyDiceHandler enemyDice,
      IDiceRoller diceRoller,
      IPickTarget pickTarget)
    {
      _diceMover = diceMover;
      _enemyHandler = enemyHandler;
      _enemyDice = enemyDice;
      _diceRoller = diceRoller;
      _pickTarget = pickTarget;
    }

    public async void Enter()
    {
      await _diceMover.ToBoard(_enemyDice.EnemyDice);
      await _diceRoller.Role();
      await _diceMover.ToCard(_enemyDice.EnemyDice);
      await _pickTarget.SelectTarget(_enemyHandler.Card);

      ChangeState?.Invoke(typeof(PlayerRoll));
    }

    public void Exit()
    {
      
    }
  }
}