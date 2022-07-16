using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Code.Game
{
  public class CardPositioner
  {
    private readonly ILoadLevel _loadLevel;
    private readonly IEnemyHandler _enemyHandler;
    private readonly IPlayerHandler _playerHandler;
    private readonly Settings _settings;

    public CardPositioner(
      ILoadLevel loadLevel,
      IEnemyHandler enemyHandler,
      IPlayerHandler playerHandler,
      Settings settings)
    {
      _loadLevel = loadLevel;
      _enemyHandler = enemyHandler;
      _playerHandler = playerHandler;
      _settings = settings;

      _loadLevel.Complete += Initialize;
    }

    private void Initialize()
    {
      _loadLevel.Complete -= Initialize;

      CalculatePosition(_playerHandler.PlayerCard);
      CalculatePosition(_enemyHandler.EnemyCard);
    }

    private void CalculatePosition(List<GameObject> cards)
    {
      float border = ((cards.Count + (cards.Count - 1) * _settings.Offset) - 1) / 2;
      
      for (int i = 0, end = cards.Count - 1; i <= end; ++i)
      {
        float endValue = Mathf.Lerp(-border, border, (float)i / end);
        cards[i].transform.DOLocalMoveX(endValue, _settings.Duration);
      }
    }

    [Serializable]
    public class Settings
    {
      public float Offset;
      public float Duration;
    }
  }
}