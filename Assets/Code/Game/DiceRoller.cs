using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Code.Facade;
using UnityEngine;

namespace Code.Game
{
  public interface IDiceRoller
  {
    Task Role(List<DiceFacade> dice);
  }

  public class DiceRoller : IDiceRoller
  {
    public DiceRoller()
    {
    }

    public async Task Role(List<DiceFacade> dice)
    {
      foreach (DiceFacade die in dice) 
        die.Die.Roll();

      while (dice.Any(x => x.Die.isRolling)) 
        await Task.Yield();

      Debug.Log("Wait complete!");
    }
  }
}