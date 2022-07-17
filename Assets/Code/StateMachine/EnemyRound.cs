using System;
using Code.Game;
using UnityEngine;

namespace Code.StateMachine
{
  public class EnemyRound : IState
  {
    public event Action<Type> ChangeState;
    
    private readonly IDiceMover _diceMover;
    private readonly ICardPositioner _cardPositioner;
    private readonly IEnemyHandler _enemyHandler;
    private readonly IPlayerHandler _playerHandler;
    private readonly IEnemyDiceHandler _enemyDice;
    private readonly IDiceRoller _diceRoller;
    private readonly IPickTarget _pickTarget;

    public EnemyRound(
      IDiceMover diceMover,
      ICardPositioner cardPositioner,
      IEnemyHandler enemyHandler,
      IPlayerHandler playerHandler,
      IEnemyDiceHandler enemyDice,
      IDiceRoller diceRoller,
      IPickTarget pickTarget)
    {
      _diceMover = diceMover;
      _cardPositioner = cardPositioner;
      _enemyHandler = enemyHandler;
      _playerHandler = playerHandler;
      _enemyDice = enemyDice;
      _diceRoller = diceRoller;
      _pickTarget = pickTarget;
    }

    public async  void Enter()
    {
      await _cardPositioner.CalculatePosition(_enemyHandler.Card);
      await _diceMover.ToBoard(_enemyDice.EnemyDice);
      await _diceRoller.Role(_enemyDice.EnemyDice);
      await _diceMover.ToCard(_enemyDice.EnemyDice);
      await _pickTarget.SelectTarget(_enemyHandler.Card);
    }

    public void Exit()
    {
      
    }
  }
}