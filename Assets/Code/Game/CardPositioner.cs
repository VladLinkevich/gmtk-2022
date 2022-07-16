using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Code.Facade;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Code.Game
{
  public interface ICardPositioner
  {
    Task CalculatePosition(List<CardFacade> cards);
  }

  public class CardPositioner : ICardPositioner
  {
    private readonly Settings _settings;

    public CardPositioner(
      IEnemyHandler enemyHandler,
      IPlayerHandler playerHandler,
      Settings settings)
    {
      _settings = settings;
    }


    public Task CalculatePosition(List<CardFacade> cards)
    {
      Tween tween = null;
      float border = ((cards.Count + (cards.Count - 1) * _settings.Offset) - 1) / 2;
      
      for (int i = 0, end = cards.Count - 1; i <= end; ++i)
      {
        float endValue = Mathf.Lerp(-border, border, (float)i / end);
        tween = cards[i].transform.DOLocalMoveX(endValue, _settings.Duration);
      }
      
      return tween.AsyncWaitForCompletion();
    }

    [Serializable]
    public class Settings
    {
      public float Offset;
      public float Duration;
    }
  }
}