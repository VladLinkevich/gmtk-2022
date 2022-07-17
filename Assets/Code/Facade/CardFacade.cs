using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Facade
{
  public class CardFacade : MonoBehaviour
  {
    public event Action<CardFacade> Enter;
    public event Action<CardFacade> Exit;
    public event Action<CardFacade> Down;
    public event Action<CardFacade> Up;
    
    [ReadOnly]
    public Guid Guid;
    public SpriteRenderer Character;
    public MeshRenderer Label;
    public DiceFacade DiceFacade;
    public HpBarFacade HpBarFacade;
    public MouseObserver MouseObserver;

    public event Action<CardFacade> DestroyCard;

    private void OnEnable()
    {
      MouseObserver.Enter += OnEnter;
      MouseObserver.Exit += OnExit;
      MouseObserver.Down += OnDown;
      MouseObserver.Up += OnUp;
      HpBarFacade.Destroy += LoseHp;
    }

    private void OnDisable()
    {
      MouseObserver.Enter -= OnEnter;
      MouseObserver.Exit -= OnExit;
      MouseObserver.Down -= OnDown;
      MouseObserver.Up -= OnUp;
      HpBarFacade.Destroy -= LoseHp;
    }

    private void LoseHp() => 
      Destroy(gameObject);

    private void OnDestroy() => 
      DestroyCard?.Invoke(this);

    private void OnDown() => 
      Down?.Invoke(this);

    private void OnExit() => 
      Exit?.Invoke(this);

    private void OnEnter() => 
      Enter?.Invoke(this);

    private void OnUp() => 
      Up?.Invoke(this);
  }
}