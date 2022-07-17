using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Facade
{
  public class EnterObserver : MonoBehaviour
  {
    public bool Ignore = true;

    public event Action Enter;
    public event Action Exit;
    
    private void OnMouseEnter()
    {
      if (Ignore ||
          Mouse.current.leftButton.isPressed)
        return;
      
      Enter?.Invoke();
    }

    private void OnMouseExit()
    {
      if (Ignore ||
          Mouse.current.leftButton.isPressed)
        return;
      
      Exit?.Invoke();
    }
  }
}