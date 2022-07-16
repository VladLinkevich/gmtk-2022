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
    private readonly IEnemyDiceHandler _enemyDice;
    private readonly IDiceRoller _diceRoller;

    public EnemyRound(
      IDiceMover diceMover,
      ICardPositioner cardPositioner,
      IEnemyHandler enemyHandler,
      IEnemyDiceHandler enemyDice,
      IDiceRoller diceRoller)
    {
      _diceMover = diceMover;
      _cardPositioner = cardPositioner;
      _enemyHandler = enemyHandler;
      _enemyDice = enemyDice;
      _diceRoller = diceRoller;
    }

    public async  void Enter()
    {
      await _cardPositioner.CalculatePosition(_enemyHandler.EnemyCard);
      await _diceMover.ToBoard(_enemyDice.EnemyDice);
      await _diceRoller.Role(_enemyDice.EnemyDice);
      await _diceMover.ToCard(_enemyDice.EnemyDice);
      
    }

    public void Exit()
    {
      
    }
  }
}