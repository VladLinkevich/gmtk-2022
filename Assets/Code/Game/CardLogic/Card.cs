using System;
using Code.Facade;
using UnityEngine;

namespace Code.Game.CardLogic
{
  public interface ICard
  {
    public event Action<ICard> Destroy;
    public GameObject Instance { get; }
    public Transform Transform { get; }
    public DiceFacade Dice { get; }
  }

  public class Card : ICard 
  {
    public event Action<ICard> Destroy;
    
    private readonly CardFacade _facade;
    
    public GameObject Instance => _facade.gameObject;
    public Transform Transform => _facade.transform;
    public DiceFacade Dice => _facade.DiceFacade;

    
    public Card(
      CardFacade facade)
    {
      _facade = facade;
    }
  }
}