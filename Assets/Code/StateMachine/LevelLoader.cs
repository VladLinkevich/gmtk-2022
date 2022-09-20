using System;
using Code.Data;
using Code.Game.CardLogic;
using Code.StateMachine;
using UnityEngine;

namespace Code.Game
{

  public class LevelLoader : IState
  {
    public event Action<Type> ChangeState;

    public event Action Complete;

    private readonly ICardFactory _cardFactory;

    public LevelLoader(
      ICardFactory cardFactory)
    {
      _cardFactory = cardFactory;
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
      

      _cardFactory.CreatePlayerCard(CardType.Archer);
      _cardFactory.CreatePlayerCard(CardType.Mage);
      _cardFactory.CreatePlayerCard(CardType.Rouge);
      _cardFactory.CreatePlayerCard(CardType.Warrior);
      
      ChangeState?.Invoke(typeof(PreviewState));
    }

    public void Exit()
    {
    }

    public void Initialize()
    {

      //_cardFactory.CreateEnemyCard(CardType.SkullArcher);
      //_cardFactory.CreateEnemyCard(CardType.SkullCommon);

      Complete?.Invoke();
    }

    private void LevelOne()
    {
      _cardFactory.CreateEnemyCard(CardType.SkullCommon);
    }

    private void LevelTwo()
    {
      _cardFactory.CreateEnemyCard(CardType.SkullCommon);
      _cardFactory.CreateEnemyCard(CardType.SkullArcher);
      _cardFactory.CreateEnemyCard(CardType.SkullCommon);
    }

    private void LevelTree()
    {
      _cardFactory.CreateEnemyCard(CardType.SkullCommon);
      _cardFactory.CreateEnemyCard(CardType.SkullArcher);
      _cardFactory.CreateEnemyCard(CardType.OgrWarrior);
    }
    
    private void LevelFour()
    {
      _cardFactory.CreateEnemyCard(CardType.OgrWarrior);
      _cardFactory.CreateEnemyCard(CardType.SkullMage);
      _cardFactory.CreateEnemyCard(CardType.OgrWarrior);
    }

    private void LevelFive()
    {
      _cardFactory.CreateEnemyCard(CardType.OgrBerserk);
      _cardFactory.CreateEnemyCard(CardType.SkullArcher);
      _cardFactory.CreateEnemyCard(CardType.SkullMage);
      _cardFactory.CreateEnemyCard(CardType.OgrBerserk);
    }
    
    private void LevelSix()
    {
      _cardFactory.CreateEnemyCard(CardType.SkullMage);
      _cardFactory.CreateEnemyCard(CardType.OgrBerserk);
      _cardFactory.CreateEnemyCard(CardType.OgrBerserk);
      _cardFactory.CreateEnemyCard(CardType.SkullMage);
    }
    
    private void LevelSeven()
    {
      _cardFactory.CreateEnemyCard(CardType.OgrWarrior);
      _cardFactory.CreateEnemyCard(CardType.OgrBerserk);
      _cardFactory.CreateEnemyCard(CardType.OgrBerserk);
      _cardFactory.CreateEnemyCard(CardType.OgrWarrior);
    }
  }
}