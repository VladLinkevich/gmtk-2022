using System;
using System.Collections.Generic;
using Code.Facade;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Game
{
  public interface IDiceMover
  {
    void ToBoard(List<DiceFacade> dice);
  }

  public class DiceMover : IDiceMover
  {
    private const int NumJumps = 1;
    //hardcocococor
    public readonly Vector3 Bounds = new Vector3(1.5f, 1f, 2.1f);
    
    private readonly Settings _settings;

    public DiceMover(
      Settings settings)
    {
      _settings = settings;
    }

    public void ToBoard(List<DiceFacade> dice)
    {
      foreach (DiceFacade die in dice)
        MoveDie(die, GetRandomPosition())
          .OnComplete(() => die.TogglePhysic(true));
    }

    private Tween MoveDie(DiceFacade die, Vector3 position) => 
      die.transform.DOJump(position, _settings.JumpPower, NumJumps, _settings.Duration);

    private Vector3 GetRandomPosition()
    {
      return new Vector3(
        x: Random.Range(-Bounds.x, Bounds.x),
        y: Bounds.y,
        z: Random.Range(-Bounds.z, Bounds.z));
    }

    [Serializable]
    public class Settings
    {
      public float Duration;
      public float JumpPower;
    }
  }
}