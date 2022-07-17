using System;
using UnityEngine;

namespace Code.Facade
{
  public class EnterObserver : MonoBehaviour
  {
    public bool Ignore = true;

    public event Action Enter;
    public event Action Exit;
    
    private void OnMouseEnter() => 
      Enter?.Invoke();

    private void OnMouseExit() => 
      Exit?.Invoke();
  }
}