﻿using System;
using System.Collections.Generic;
using Code.Data;
using Code.Facade;
using Code.Game.CardLogic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Code.Game
{
  public interface IPickTarget
  {
    UniTask SelectTarget(List<CardFacade> cards, List<CardFacade> targets);
  }

  public class TargetSelecter : IPickTarget
  {
    private readonly IArrow _arrow;
    private readonly IActionWriter _actionWriter;
    private readonly Settings _settings;

    public TargetSelecter(
      IArrow arrow,
      IActionWriter actionWriter,
      Settings settings)
    {
      _arrow = arrow;
      _actionWriter = actionWriter;
      _settings = settings;
    }

    public async UniTask SelectTarget(List<CardFacade> cards, List<CardFacade> targets)
    {
      foreach (CardFacade card in cards)
      {
        DiceFacade dice = card.DiceFacade;
        if (((SideAction) dice.Current.Type & SideAction.Attack) == SideAction.Attack)
        {
          CardFacade target = GetTarget(targets);
          
          await AnimateArrow(dice, target);
          _actionWriter.Write(card, target);
          
          card.gameObject
            .GetComponent<EnemyTargetView>()
            .Setup(_arrow.Enemy, target.Transform.position);
        }
      }
    }

    private async UniTask AnimateArrow(DiceFacade dice, CardFacade target)
    {
      float time = 0;
      
      Vector3 start = dice.transform.position;
      Vector3 end = target.Transform.position;

      _arrow.Enemy.gameObject.SetActive(true);
      _arrow.Enemy.SetPositions(start, start);
      foreach (MeshRenderer renderer in _arrow.Enemy.renderers) 
        renderer.material.color = _settings.Color;

      while (time < _settings.DurationMove)
      {
        Vector3 current = Vector3.Lerp(start, end, time / _settings.DurationMove);
        _arrow.Enemy.SetPositions(start, current);

        await UniTask.NextFrame();
        time += Time.deltaTime;
      }

      Tween tween = null;
      Color color = _settings.Color;
      color.a = 0;
      foreach (MeshRenderer renderer in _arrow.Enemy.renderers)
        tween = renderer.material.DOColor(color, _settings.DurationBetween);
      
      await tween;
      _arrow.Enemy.gameObject.SetActive(false);
    }


    private CardFacade GetTarget(List<CardFacade> targets) => 
      targets[UnityEngine.Random.Range(0, targets.Count)];

    [Serializable]
    public class Settings
    {
      public float DurationMove;
      public float DurationBetween;
      public Color Color;
    }
  }
}