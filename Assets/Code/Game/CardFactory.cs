using System;
using System.Collections.Generic;
using Code.Data;
using Code.Facade;
using Code.Services.ResourceLoadService;
using Code.StaticData;
using UnityEngine;

namespace Code.Game
{
  public interface ICardFactory
  {
    GameObject CreatePlayerCard(CardType type);
    GameObject CreateEnemyCard(CardType type);
  }

  public interface IEnemyHandler
  {
    event Action<CardFacade> EnemyCardCreate;
    List<CardFacade> EnemyCard { get; }
  }

  public interface IPlayerHandler
  {
    event Action<CardFacade> PlayerCardCreate;
    List<CardFacade> PlayerCard { get; }
  }

  public interface IPlayerDiceHandler
  {
    List<DiceFacade> PlayerDice { get; }
  }

  public interface IEnemyDiceHandler
  {
    List<DiceFacade> EnemyDice { get; }
  }

  public class CardFactory : ICardFactory, IEnemyHandler, IPlayerHandler, IPlayerDiceHandler, IEnemyDiceHandler
  {
    private const string PlayerRootTag = "player_root";
    private const string EnemyRootTag = "enemy_root";
    
    public event Action<CardFacade> PlayerCardCreate;
    public event Action<CardFacade> EnemyCardCreate;

    public List<CardFacade> PlayerCard { get; private set; } = new List<CardFacade>();
    public List<CardFacade> EnemyCard { get; private set; } = new List<CardFacade>();
    
    public List<DiceFacade> PlayerDice { get; private set; } = new List<DiceFacade>();
    public List<DiceFacade> EnemyDice { get; private set; } = new List<DiceFacade>();

    private readonly IResourceLoader _loader;
    private readonly CardHandler _dataHandler;
    private readonly SideHandler _sideHandler;
    private readonly Settings _settings;

    private Transform _playerRoot;
    private Transform _enemyRoot;

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

      GetCardsRoot();
    }

    public GameObject CreatePlayerCard(CardType type)
    {
      CardData data = _dataHandler.GetCardData(type);
      GameObject card = UnityEngine.Object.Instantiate(_settings.PlayerCard, _playerRoot);

      CardFacade facade = card.GetComponent<CardFacade>();
      
      SetupCardFacade(facade, data);
      SetupDice(facade.DiceFacade, data);

      PlayerCard.Add(facade);
      PlayerDice.Add(facade.DiceFacade);
      
      PlayerCardCreate?.Invoke(facade);
      
      return card;
    }

    public GameObject CreateEnemyCard(CardType type)
    {
        CardData data = _dataHandler.GetCardData(type);
        GameObject card = UnityEngine.Object.Instantiate(_settings.EnemyCard, _enemyRoot);

        CardFacade facade = card.GetComponent<CardFacade>();
        SetupCardFacade(facade, data);
        SetupDice(facade.DiceFacade, data);

        EnemyCard.Add(facade);
        EnemyDice.Add(facade.DiceFacade);
        EnemyCardCreate?.Invoke(facade);
      
        return card;
    }

    private void SetupCardFacade(CardFacade facade, CardData data)
    {
      facade.Character.sprite = data.Character;
      facade.Label.material.color = data.Color;
      facade.HpBarFacade.Set(data.Hp);
    }

    private void SetupDice(DiceFacade facade, CardData data)
    {
      facade.Renderer.material.color = data.Color;
      for (int i = 0, end = data.Sides.Length; i < end; ++i)
      {
        SideStaticData staticData = _sideHandler.GetSideData(data.Sides[i].Type);
        
        facade.Sides[i].Renderer.sprite = staticData.Icon;
        facade.Sides[i].Value.Set(data.Sides[i].Value);
      }
    }


    private void GetCardsRoot()
    {
      _playerRoot = UnityEngine.GameObject.FindGameObjectWithTag(PlayerRootTag).transform;
      _enemyRoot = UnityEngine.GameObject.FindGameObjectWithTag(EnemyRootTag).transform;
    }
    
    [Serializable]
    public class Settings
    {
      public GameObject PlayerCard;
      public GameObject EnemyCard;
    }
  }
}