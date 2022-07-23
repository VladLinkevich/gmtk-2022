using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Facade
{
  public class MouseObserver : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler,
    IPointerExitHandler
  {
    public event Action Enter;
    public event Action Exit;
    public event Action Down;
    public event Action Up;

    public bool Ignore;


    public void OnPointerDown(PointerEventData eventData)
    {
      if (Ignore) return;

      Down?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
      if (Ignore) return;

      Up?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      if (Ignore) return;

      Enter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      if (Ignore) return;

      Exit?.Invoke();
    }
  }
}