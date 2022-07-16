using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Code.Facade;
using Code.Services.CoroutineRunnerService;
using UnityEngine;

namespace Code.Game
{
  public class PickTarget
  {
    private readonly IArrow _arrow;
    private readonly ICoroutineRunner _runner;

    public PickTarget(
      IArrow arrow,
      ICoroutineRunner runner)
    {
      _arrow = arrow;
      _runner = runner;
    }

    public async Task SelectCardTarget(List<CardFacade> cards, List<CardFacade> targets)
    {
      foreach (CardFacade card in cards)
      {
        CardFacade target = GetTarget(targets);
        Coroutine coroutine = _runner.StartCoroutine(ArrowAnimation());
        
        //target.transform.position;
      }
    }

    private IEnumerator ArrowAnimation()
    {
      yield break;
    }

    private CardFacade GetTarget(List<CardFacade> targets) => 
      targets[UnityEngine.Random.Range(0, targets.Count)];
  }
}