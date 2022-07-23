using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Facade
{
  public class ObserveOnClick : MonoBehaviour, IPointerClickHandler
  {
    public event Action Click;
    
    public bool Ignore;

    public void OnPointerClick(PointerEventData eventData)
    {
      if (Ignore) return;
      
      Click?.Invoke();
    }
  }
}