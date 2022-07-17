using System;
using UnityEngine;

namespace Code.Facade
{
  public class ObserveOnClick : MonoBehaviour
  {
    public event Action Click;
    
    public bool Ignore;
    
    private void OnMouseUpAsButton()
    {
      if (Ignore) return;
      
      Click?.Invoke();
    }
  }
}