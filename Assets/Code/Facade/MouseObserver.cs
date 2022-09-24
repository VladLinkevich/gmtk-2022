using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Facade
{
  public class MouseObserver : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler,
    IPointerExitHandler
  {
    public CardFacade Card;
    
    public event Action<CardFacade> Enter;
    public event Action<CardFacade> Exit;
    public event Action<CardFacade> Down;
    public event Action<CardFacade> Up;

    public bool Ignore;


    public void OnPointerDown(PointerEventData eventData)
    {
      if (Ignore) return;

      Down?.Invoke(Card);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
      if (Ignore) return;

      Up?.Invoke(Card);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      if (Ignore) return;

      Enter?.Invoke(Card);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      if (Ignore) return;

      Exit?.Invoke(Card);
    }
  }
}