using System;
using System.Collections.Generic;
using Code.Facade;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Game
{
  public interface IDiceMover
  {
    List<DiceFacade> DiceOnBoard { get; }
    UniTask ToBoard(List<DiceFacade> dice);
    UniTask ToCard(List<DiceFacade> dice);
  }

  public class DiceMover : IDiceMover
  {
    private const int NumJumps = 1;
    //hardcocococor
    public readonly Vector3 Bounds = new Vector3(1.5f, 0f, 2.1f);
    public readonly Vector3 CardPosition = new Vector3(0f, 0f, 0.2f);
    
    private readonly Settings _settings;

    public List<DiceFacade> DiceOnBoard { get; private set; } = new();

    public DiceMover(
      Settings settings)
    {
      _settings = settings;
    }

    public async UniTask ToBoard(List<DiceFacade> dice)
    {
      Tween tween = null;
      
      foreach (DiceFacade die in dice)
      {
        DiceOnBoard.Add(die);
        die.Click += ToggleMove;
        
        tween = MoveDie(die, GetRandomPosition())
          .OnComplete(() => die.ActivatePhysic(true));
      }

      await tween;
    }

    public async UniTask ToCard(List<DiceFacade> dice)
    {
      Tween tween = null;
      
      foreach (DiceFacade die in dice)
      {
        DiceOnBoard.Remove(die);
        die.ActivatePhysic(false);
        die.Click -= ToggleMove;

        tween = MoveToCard(die);
      }

      await tween;
    }

    private void ToggleMove(DiceFacade die)
    {
      if (DiceOnBoard.Contains(die))
        ClickToCard(die);
      else
        ClickToBoard(die);
    }

    private void ClickToBoard(DiceFacade die)
    {
      DiceOnBoard.Add(die);

      die.Observe.Ignore = !false;

      RotateToCard(die, die.SaveRotation.y);
      MoveDie(die, die.SavePosition)
        .OnComplete(() =>
        {
          die.Observe.Ignore = !true;
          die.ActivatePhysic(true);
        });
    }

    private void ClickToCard(DiceFacade die)
    {
      DiceOnBoard.Remove(die);

      die.SavePositionAndRotation();
      die.Observe.Ignore = !false;
      die.ActivatePhysic(false);

      MoveToCard(die)
        .OnComplete(() => die.Observe.Ignore = !true);
    }

    private Tween MoveToCard(DiceFacade die)
    {
      RotateToCard(die);
      return MoveLocalDie(die, CardPosition);
    }

    private void RotateToCard(DiceFacade die, float angle = 180f)
    {
      Vector3 angles = die.transform.eulerAngles;
      angles.y = angle;
      RotateToIdentity(die, angles);
    }

    private Tween MoveDie(DiceFacade die, Vector3 position) => 
      die.transform.DOJump(position, _settings.JumpPower, NumJumps, _settings.Duration);

    private Tween MoveLocalDie(DiceFacade die, Vector3 position) => 
      die.transform.DOLocalJump(position, _settings.JumpPower, NumJumps, _settings.Duration);

    private void RotateToIdentity(DiceFacade die, Vector3 angle) => 
      die.transform.DORotate(angle, _settings.Duration);

    private Vector3 GetRandomPosition() =>
      new Vector3(
        x: Random.Range(-Bounds.x, Bounds.x),
        y: Bounds.y,
        z: Random.Range(-Bounds.z, Bounds.z));

    [Serializable]
    public class Settings
    {
      public float Duration;
      public float JumpPower;
    }
  }
}