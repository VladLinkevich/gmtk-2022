using System;
using System.Collections.Generic;
using Code.Data;
using Code.Facade;
using UnityEngine;

namespace Code.Game.CardLogic
{
  
  public interface IDeck
  {
    List<CardFacade> Card { get; }
    void Add(CardType type);
  }
  
  public class Deck 
  {
    private readonly ICardFactory _cardFactory;
    private readonly ICardPositioner _cardPositioner;
    private readonly Settings _settings;
    private Transform _root;

    public List<CardFacade> Card { get; private set; } = new();

    public Deck(
      ICardFactory cardFactory,
      ICardPositioner cardPositioner,
      Settings settings)
    {
      _cardFactory = cardFactory;
      _cardPositioner = cardPositioner;
      _settings = settings;

      SetupRoot();
    }

    private void SetupRoot()
    {
      _root = new GameObject(nameof(PlayerDeck)).transform;
      _root.position = _settings.Position;
      _root.eulerAngles = _settings.Rotation;
    }

    public void Add(CardType type)
    {
      CardFacade facade = _cardFactory.CreateCard(_settings.Prefab, type, _root);
      Card.Add(facade);
      _cardPositioner.CalculatePosition(Card);
    }

    [Serializable]
    public class Settings
    {
      public GameObject Prefab;

      public Vector3 Position;
      public Vector3 Rotation;
    }
  }
}