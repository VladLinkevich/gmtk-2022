using System;
using Code.Data;
using Code.Facade;
using Code.Services.ResourceLoadService;
using Code.StaticData;
using UnityEngine;

namespace Code.Game
{
  public interface ICardFactory
  {
    event Action<GameObject> PlayerCardCreate;
    GameObject CreatePlayerCard(CardType type);
  }

  public class CardFactory : ICardFactory
  {
    private const string PlayerRootTag = "player_root";
    private const string EnemyRootTag = "enemy_root";
    
    public event Action<GameObject> PlayerCardCreate;
    
    private readonly IResourceLoader _loader;
    private readonly CardHandler _dataHandler;
    private readonly Settings _settings;

    private Transform _playerRoot;
    private Transform _enemyRoot;

    public CardFactory(
      IResourceLoader loader,
      CardHandler dataHandler,
      Settings settings)
    {
      _loader = loader;
      _dataHandler = dataHandler;
      _settings = settings;

      GetCardsRoot();
    }

    public GameObject CreatePlayerCard(CardType type)
    {
      CardData data = _dataHandler.GetCardData(type);
      GameObject card = UnityEngine.Object.Instantiate(_settings.PlayerCard);

      CardFacade facade = card.GetComponent<CardFacade>();
      facade.Character.sprite = data.Character;
      facade.Label.material.color = data.Color;

      PlayerCardCreate?.Invoke(card);
      
      return card;
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