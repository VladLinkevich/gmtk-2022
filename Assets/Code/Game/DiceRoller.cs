using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Code.Facade;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Game
{
  public interface IDiceRoller
  {
    UniTask Role(List<DiceFacade> dice);
  }

  public class DiceRoller : IDiceRoller
  {
    public DiceRoller()
    {
    }

    public async UniTask Role(List<DiceFacade> dice)
    {
      foreach (DiceFacade die in dice) 
        die.Die.Roll();

      while (dice.Any(x => x.Die.isRolling)) 
        await UniTask.NextFrame();

      Debug.Log("Wait complete!");
    }
  }
}