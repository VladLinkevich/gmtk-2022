using System;
using System.Collections.Generic;
using Code.Data;
using Code.Facade;
using Code.Services.ResourceLoadService;
using Code.StaticData;
using UnityEngine;

namespace Code.Game.CardLogic
{
  public interface ICardFactory
  {
    CardFacade CreateCard(CardType type);
  }

  public interface IEnemyHandler
  {
    event Action<CardFacade> EnemyCardCreate;
    List<CardFacade> Card { get; }
  }

  public interface IEnemyDiceHandler
  {
    List<DiceFacade> EnemyDice { get; }
  }

  public class CardFactory : ICardFactory, IEnemyHandler, IEnemyDiceHandler
  {
    public event Action<CardFacade> EnemyCardCreate;
    
    List<CardFacade> IEnemyHandler.Card => _enemyCard;
    
    public List<DiceFacade> PlayerDice { get; private set; } = new List<DiceFacade>();
    public List<DiceFacade> EnemyDice { get; private set; } = new List<DiceFacade>();

    private readonly IResourceLoader _loader;
    private readonly CardHandler _dataHandler;
    private readonly SideHandler _sideHandler;
    private readonly Settings _settings;

    private readonly List<CardFacade> _enemyCard = new List<CardFacade>();

    private Transform _root;

    public CardFactory(
      IResourceLoader loader,
      CardHandler dataHandler,
      SideHandler sideHandler,
      Settings settings)
    {
      _loader = loader;
      _dataHandler = dataHandler;
      _sideHandler = sideHandler;
      _settings = settings;

      CreateFactoryRoot();
    }

    private void CreateFactoryRoot()
    {
      _root = new GameObject(nameof(CardFactory)).transform;
    }

    public CardFacade CreateCard(CardType type)
    {
      GameObject prefab = UnityEngine.Object.Instantiate(_settings.PlayerCard, _root);
      CardFacade facade = SetupCard(prefab, type);

      return facade;
    }

    public GameObject CreateEnemyCard(CardType type)
    {
        GameObject prefab = UnityEngine.Object.Instantiate(_settings.EnemyCard, _root);
        CardFacade facade = SetupCard(prefab, type);

        _enemyCard.Add(facade);
        EnemyDice.Add(facade.DiceFacade);
        EnemyCardCreate?.Invoke(facade);
      
        return prefab;
    }

    private CardFacade SetupCard(GameObject prefab, CardType type)
    {
      CardData data = _dataHandler.GetCardData(type);
      CardFacade facade = prefab.GetComponent<CardFacade>();

      SetupCardFacade(facade, data);
      SetupDice(facade.DiceFacade, data);
      
      return facade;
    }

    private void SetupCardFacade(CardFacade facade, CardData data)
    {
      facade.Guid = Guid.NewGuid();
      facade.Character.sprite = data.Character;
      facade.Label.material.color = data.Color;
      facade.HpBarFacade.Set(data.Hp);
    }

    private void SetupDice(DiceFacade facade, CardData data)
    {
      facade.Type = data.Type;
      facade.Renderer.material.color = data.Color;
      for (int i = 0, end = data.Sides.Length; i < end; ++i)
      {
        SideStaticData staticData = _sideHandler.GetSideData(data.Sides[i].Type);

        facade.Sides[i].Type = staticData.Type;
        facade.Sides[i].Renderer.sprite = staticData.Icon;
        facade.Sides[i].Value.Set(data.Sides[i].Value);
      }
    }
    


    [Serializable]
    public class Settings
    {
      public GameObject PlayerCard;
      public GameObject EnemyCard;
    }
  }
}