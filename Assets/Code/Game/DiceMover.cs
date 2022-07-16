﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Code.Facade;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Game
{
  public interface IDiceMover
  {
    Task ToBoard(List<DiceFacade> dice);
    Task ToCard(List<DiceFacade> dice);
  }

  public class DiceMover : IDiceMover
  {
    private const int NumJumps = 1;
    //hardcocococor
    public readonly Vector3 Bounds = new Vector3(1.5f, 0f, 2.1f);
    public readonly Vector3 CardPosition = new Vector3(0f, 0f, 0.2f);
    
    private readonly Settings _settings;

    public DiceMover(
      Settings settings)
    {
      _settings = settings;
    }

    public Task ToBoard(List<DiceFacade> dice)
    {
      Tween tween = null;
      
      foreach (DiceFacade die in dice)
        tween = MoveDie(die, GetRandomPosition())
          .OnComplete(() => die.TogglePhysic(true));

      return tween.AsyncWaitForCompletion();
    }
    
    public Task ToCard(List<DiceFacade> dice)
    {
      Tween tween = null;
      
      foreach (DiceFacade die in dice)
      {
        die.TogglePhysic(false);
        RotateToCard(die);
        tween = MoveLocalDie(die, CardPosition);
      }

      return tween.AsyncWaitForCompletion();
    }

    private void RotateToCard(DiceFacade die)
    {
      Vector3 angles = die.transform.eulerAngles;
      angles.y = 180;
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