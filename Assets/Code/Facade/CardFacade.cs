using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Facade
{
  public class CardFacade : MonoBehaviour
  {
    [ReadOnly]
    public Guid Guid;
    public SpriteRenderer Character;
    public MeshRenderer Label;
    public DiceFacade DiceFacade;
    public HpBarFacade HpBarFacade;

    public event Action<CardFacade> DestroyCard;

    private void OnDestroy() => 
      DestroyCard?.Invoke(this);
  }
}