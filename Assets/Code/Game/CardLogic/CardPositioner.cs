using System;
using System.Collections.Generic;
using Code.Facade;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Code.Game.CardLogic
{
  public interface ICardPositioner
  {
    UniTask CalculatePosition(List<CardFacade> cards);
  }

  public class CardPositioner : ICardPositioner
  {
    private readonly Settings _settings;

    public CardPositioner(
      Settings settings)
    {
      _settings = settings;
    }


    public async UniTask CalculatePosition(List<CardFacade> cards)
    {
      if (cards.Count == 1) return;
      
      Tween tween = null;
      float border = ((cards.Count + (cards.Count - 1) * _settings.Offset) - 1) / 2;

      for (int i = 0, end = cards.Count - 1; i <= end; ++i)
      {
        float endValue = Mathf.Lerp(-border, border, (float)i / end);
        tween = cards[i].Transform.DOLocalMoveX(endValue, _settings.Duration);
      }
      
      await tween;
    }

    [Serializable]
    public class Settings
    {
      public float Offset;
      public float Duration;
    }
  }
}