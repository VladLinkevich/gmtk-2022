using System;
using Code.Game;

namespace Code.StateMachine
{
  public class EnemyRound : IState
  {
    public event Action<Type> ChangeState;
    
    private readonly IDiceMover _diceMover;
    private readonly IEnemyDiceHandler _enemyDice;

    public EnemyRound(
      IDiceMover diceMover,
      IEnemyDiceHandler enemyDice)
    {
      _diceMover = diceMover;
      _enemyDice = enemyDice;
    }

    public void Enter()
    {
      _diceMover.ToBoard(_enemyDice.EnemyDice); 
    }

    public void Exit()
    {
      
    }
  }
}