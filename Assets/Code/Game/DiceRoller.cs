using System.Collections.Generic;
using System.Linq;
using Code.Facade;
using Cysharp.Threading.Tasks;

namespace Code.Game
{
  public interface IDiceRoller
  {
    UniTask Role();
  }

  public class DiceRoller : IDiceRoller
  {
    private readonly IDiceMover _diceMover;

    public DiceRoller(IDiceMover diceMover)
    {
      _diceMover = diceMover;
    }

    public async UniTask Role()
    {
      List<DiceFacade> dice = _diceMover.DiceOnBoard;
      
      foreach (DiceFacade die in dice) 
        die.Die.Roll();

      while (dice.Any(x => x.Die.isRolling)) 
        await UniTask.NextFrame();
    }
  }
}