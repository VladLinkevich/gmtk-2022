using System;
using Code.Data;
using Code.Facade;
using Code.Services.ResourceLoadService;
using Code.StaticData;
using Unity.VisualScripting;
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
      facade.Character.sprite = data.Character;
      facade.Label.material.color = data.Color;

      SetupDice(facade.DiceFacade, data.Sides, data.Color);

      PlayerCardCreate?.Invoke(card);
      
      return card;
    }

    private void SetupDice(DiceFacade facade, SideData[] sides, Color color)
    {
      facade.Renderer.material.color = color;
      for (int i = 0, end = sides.Length; i < end; ++i)
      {
        SideStaticData data = _sideHandler.GetCardData(sides[i].Type);
        
        facade.Sides[i].Renderer.sprite = data.Icon;
        facade.Sides[i].Value.Set(sides[i].Value);
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