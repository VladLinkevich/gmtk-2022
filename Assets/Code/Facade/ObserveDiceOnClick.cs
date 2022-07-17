using System;
using UnityEngine;

namespace Code.Facade
{
  public class ObserveDiceOnClick : MonoBehaviour
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