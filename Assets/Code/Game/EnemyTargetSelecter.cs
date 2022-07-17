using System;
using System.Collections.Generic;
using Code.Data;
using Code.Facade;
using Code.Services.CoroutineRunnerService;
using Code.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Game
{
  public interface IPickTarget
  {
    UniTask SelectTarget(List<CardFacade> cards);
  }

  public class EnemyTargetSelecter : IPickTarget
  {
    private readonly IArrow _arrow;
    private readonly IPlayerHandler _playerHandler;
    private readonly IActionWriter _actionWriter;
    private readonly Settings _settings;

    public EnemyTargetSelecter(
      IArrow arrow,
      IPlayerHandler playerHandler,
      IActionWriter actionWriter,
      Settings settings)
    {
      _arrow = arrow;
      _playerHandler = playerHandler;
      _actionWriter = actionWriter;
      _settings = settings;
    }

    public async UniTask SelectTarget(List<CardFacade> cards)
    {
      foreach (CardFacade card in cards)
      {
        DiceFacade dice = card.DiceFacade;
        if (((SideAction) dice.Current.Type & SideAction.Attack) == SideAction.Attack)
        {
          CardFacade target = GetTarget(_playerHandler.Card);
          await AnimateArrow(dice, target);
          _actionWriter.Write(card, target);
        }
      }
    }

    private async UniTask AnimateArrow(DiceFacade dice, CardFacade target)
    {
      float time = 0;
      
      Vector3 start = dice.transform.position;
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