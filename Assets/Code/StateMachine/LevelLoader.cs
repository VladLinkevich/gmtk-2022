using System;
using Code.Data;
using Code.Game.CardLogic;
using Code.StateMachine;
using UnityEngine;
using Zenject;

namespace Code.Game
{

  public class LevelLoader : IState
  {
    public event Action<Type> ChangeState;
    
    private readonly  IPlayerDeck _player;
    private readonly IEnemyDeck _enemy;

    public LevelLoader(
      IPlayerDeck player,
      IEnemyDeck enemy)
    {
      _player = player;
      _enemy = enemy;
    }
    
    public void Enter()
    {
      int level = PlayerPrefs.GetInt("level", 0);
      level %= 7;
      
      if (level == 0) LevelOne();
      if (level == 1) LevelTwo();
      if (level == 2) LevelTree();
      if (level == 3) LevelFour();
      if (level == 4) LevelFive();
      if (level == 5) LevelSix();
      if (level == 6) LevelSeven();
      

      _player.Add(CardType.Archer);
      _player.Add(CardType.Mage);
      _player.Add(CardType.Rouge);
      _player.Add(CardType.Warrior);
      
      ChangeState?.Invoke(typeof(PreviewState));
    }

    public void Exit()
    {
    }

    private void LevelOne()
    {
      _enemy.Add(CardType.SkullCommon);
    }

    private void LevelTwo()
    {
      _enemy.Add(CardType.SkullCommon);
      _enemy.Add(CardType.SkullArcher);
      _enemy.Add(CardType.SkullCommon);
    }

    private void LevelTree()
    {
      _enemy.Add(CardType.SkullCommon);
      _enemy.Add(CardType.SkullArcher);
      _enemy.Add(CardType.OgrWarrior);
    }
    
    private void LevelFour()
    {
      _enemy.Add(CardType.OgrWarrior);
      _enemy.Add(CardType.SkullMage);
      _enemy.Add(CardType.OgrWarrior);
    }

    private void LevelFive()
    {
      _enemy.Add(CardType.OgrBerserk);
      _enemy.Add(CardType.SkullArcher);
      _enemy.Add(CardType.SkullMage);
      _enemy.Add(CardType.OgrBerserk);
    }
    
    private void LevelSix()
    {
      _enemy.Add(CardType.SkullMage);
      _enemy.Add(CardType.OgrBerserk);
      _enemy.Add(CardType.OgrBerserk);
      _enemy.Add(CardType.SkullMage);
    }
    
    private void LevelSeven()
    {
      _enemy.Add(CardType.OgrWarrior);
      _enemy.Add(CardType.OgrBerserk);
      _enemy.Add(CardType.OgrBerserk);
      _enemy.Add(CardType.OgrWarrior);
    }
  }
}