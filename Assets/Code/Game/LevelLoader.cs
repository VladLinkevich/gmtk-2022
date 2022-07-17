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

    public LevelLoader(ICardFactory cardFactory)
    {
      _cardFactory = cardFactory;
    }

    public void Initialize()
    {
      _cardFactory.CreatePlayerCard(CardType.Archer);
      _cardFactory.CreatePlayerCard(CardType.Mage);
      _cardFactory.CreatePlayerCard(CardType.Rouge);
      _cardFactory.CreatePlayerCard(CardType.Warrior);
      //_cardFactory.CreateEnemyCard(CardType.SkullArcher);
      //_cardFactory.CreateEnemyCard(CardType.SkullCommon);
      //_cardFactory.CreateEnemyCard(CardType.SkullCommon);
      _cardFactory.CreateEnemyCard(CardType.SkullCommon);

      Complete?.Invoke();
    }
  }
}