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
    UniTask SelectTarget(List<DiceFacade> cards);
  }

  public class EnemyTargetSelecter : IPickTarget
  {
    private readonly IArrow _arrow;
    private readonly ICoroutineRunner _runner;
    private readonly IPlayerHandler _playerHandler;
    private readonly IEnemyHandler _enemyHandler;
    private readonly CardHandler _cardHandler;
    private readonly Settings _settings;

    public EnemyTargetSelecter(
      IArrow arrow,
      ICoroutineRunner runner,
      IPlayerHandler playerHandler,
      IEnemyHandler enemyHandler,
      CardHandler cardHandler,
      Settings settings)
    {
      _arrow = arrow;
      _runner = runner;
      _playerHandler = playerHandler;
      _enemyHandler = enemyHandler;
      _cardHandler = cardHandler;
      _settings = settings;
    }

    public async UniTask SelectTarget(List<DiceFacade> dice)
    {
      foreach (DiceFacade die in dice)
      {
        CardFacade target = null;

        if (((SideAction) die.Current.Type & SideAction.Attack) == SideAction.Attack)
          target = GetTarget(_playerHandler.Card);

        if (((SideAction) die.Current.Type & SideAction.Def) == SideAction.Def)
          target = GetTarget(_enemyHandler.Card);

        if (target != null)
          await AnimateArrow(die, target);
      }
    }

    private async UniTask AnimateArrow(DiceFacade card, CardFacade target)
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