﻿using System;
using Code.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.StaticData
{
  [Serializable]
  public class SideData
  {
    [EnumToggleButtons]
    public SideType Type;
    
    [Range(1, 5)]
    [ShowIf("@Type != SideType.None")]
    public int Value = 0;
  }
}