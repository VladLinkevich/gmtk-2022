using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Facade
{
  public class CardFacade : MonoBehaviour
  {
    public event Action<CardFacade> Destroy;
    
    [ReadOnly]
    public Guid Guid;
    public SpriteRenderer Character;
    public MeshRenderer Label;
    public DiceFacade DiceFacade;
    public HpBarFacade HpBarFacade;
    public MouseObserver MouseObserver;

    public Transform Transform => transform;
  }
}