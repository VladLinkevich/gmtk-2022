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
    public event Action<GameObject> PlayerCardCreate;
    GameObject CreatePlayerCard(CardType type);
    GameObject CreateEnemyCard(CardType type);
  }

  public interface IEnemyHandler
  {
    event Action<GameObject> EnemyCardCreate;
    List<GameObject> EnemyCard { get; }
  }

  public interface IPlayerHandler
  {
    event Action<GameObject> PlayerCardCreate;
    List<GameObject> PlayerCard { get; }
  }

  public class CardFactory : ICardFactory, IEnemyHandler, IPlayerHandler
  {
    private const string PlayerRootTag = "player_root";
    private const string EnemyRootTag = "enemy_root";
    
    public event Action<GameObject> PlayerCardCreate;
    public event Action<GameObject> EnemyCardCreate;

    public List<GameObject> PlayerCard { get; private set; } = new List<GameObject>();
    public List<GameObject> EnemyCard { get; private set; } = new List<GameObject>();

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

      SetupCard(card, data);

      PlayerCard.Add(card);
      PlayerCardCreate?.Invoke(card);
      
      return card;
    }

    public GameObject CreateEnemyCard(CardType type)
    {
        CardData data = _dataHandler.GetCardData(type);
        GameObject card = UnityEngine.Object.Instantiate(_settings.EnemyCard, _enemyRoot);
        
        SetupCard(card, data);

        EnemyCard.Add(card);
        EnemyCardCreate?.Invoke(card);
      
        return card;
    }

    private void SetupCard(GameObject card, CardData data)
    {
      CardFacade facade = card.GetComponent<CardFacade>();
      SetupCardFacade(facade, data);
      SetupDice(facade.DiceFacade, data);
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