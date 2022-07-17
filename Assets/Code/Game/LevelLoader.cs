using System;
using Code.Data;
using UnityEngine;
using Zenject;

namespace Code.Game
{
  public interface ILoadLevel
  {
    event Action Complete;
  }

  public class LevelLoader : ILoadLevel, IInitializable
  {
    public event Action Complete;

    private readonly ICardFactory _cardFactory;

    public LevelLoader(
      ICardFactory cardFactory)
    {
      _cardFactory = cardFactory;
    }

    public void Initialize()
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
      _cardFactory.CreateEnemyCard(CardType.Warrior);
    }
    
    private void LevelFour()
    {
      _cardFactory.CreateEnemyCard(CardType.Warrior);
      _cardFactory.CreateEnemyCard(CardType.OgrBerserk);
      _cardFactory.CreateEnemyCard(CardType.Warrior);
    }

    private void LevelFive()
    {
      _cardFactory.CreateEnemyCard(CardType.OgrBerserk);
      _cardFactory.CreateEnemyCard(CardType.SkullArcher);
      _cardFactory.CreateEnemyCard(CardType.SkullArcher);
      _cardFactory.CreateEnemyCard(CardType.OgrBerserk);
    }
    
    private void LevelSix()
    {
      _cardFactory.CreateEnemyCard(CardType.SkullCommon);
      _cardFactory.CreateEnemyCard(CardType.OgrBerserk);
      _cardFactory.CreateEnemyCard(CardType.OgrBerserk);
      _cardFactory.CreateEnemyCard(CardType.SkullCommon);
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