using System;
using UnityEngine;

namespace Code.Facade
{
  public class MouseObserver : MonoBehaviour

  {
  public event Action Enter;
  public event Action Exit;
  public event Action Down;
  public event Action Up;

  public bool Ignore;

  private void OnMouseEnter()
  {
    if (Ignore) return;
    
    Enter?.Invoke();
  }

  private void OnMouseExit()
  {
    if (Ignore) return;

    Exit?.Invoke();
  }

  private void OnMouseDown()
  {
    if (Ignore) return;

    Debug.Log("Down");
    Down?.Invoke();
  }

  private void OnMouseUp()
  {
    if (Ignore) return;

    Debug.Log("Up");
    Up?.Invoke();
  }
  }
}