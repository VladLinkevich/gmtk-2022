using System;
using System.Collections.Generic;
using Code.Facade;
using Code.Services.CoroutineRunnerService;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Game
{
  public interface IPickTarget
  {
    UniTask SelectCardTarget(List<CardFacade> cards, List<CardFacade> targets);
  }

  public class PickTarget : IPickTarget
  {
    private readonly IArrow _arrow;
    private readonly ICoroutineRunner _runner;
    private readonly Settings _settings;

    public PickTarget(
      IArrow arrow,
      ICoroutineRunner runner,
      Settings settings)
    {
      _arrow = arrow;
      _runner = runner;
      _settings = settings;
    }

    public async UniTask SelectCardTarget(List<CardFacade> cards, List<CardFacade> targets)
    {
      foreach (CardFacade card in cards)
      {
        CardFacade target = GetTarget(targets);
        await AnimateArrow(card, target);
      }
    }

    private async UniTask AnimateArrow(CardFacade card, CardFacade target)
    {
      float time = 0;
      
      Vector3 start = card.transform.position;
      Vector3 end = target.transform.position;

      _arrow.Instance.gameObject.SetActive(true);
      _arrow.Instance.SetPositions(start, start);

      while (time < _settings.DurationMove)
      {
        Vector3 current = Vector3.Lerp(start, end, time / _settings.DurationMove);
        _arrow.Instance.SetPositions(start, current);

        await UniTask.NextFrame();
        time += Time.deltaTime;
      }

      _arrow.Instance.gameObject.SetActive(false);
      await UniTask.Delay(_settings.DelayBetween);
    }


    private CardFacade GetTarget(List<CardFacade> targets) => 
      targets[UnityEngine.Random.Range(0, targets.Count)];

    [Serializable]
    public class Settings
    {
      public float DurationMove;
      public int DelayBetween;
    }
  }
}