using System;
using Code.Data;
using Code.Facade;
using Code.Services.ResourceLoadService;
using Code.StaticData;
using UnityEngine;

namespace Code.Game.CardLogic
{
  public interface ICardFactory
  {
    CardFacade CreateCard(GameObject instance, CardType type, Transform root = null);
  }

  public class CardFactory : ICardFactory
  {
    private readonly IResourceLoader _loader;
    private readonly CardDataHandler _cardsDataHandler;
    private readonly SidesDataHandler _sidesDataHandler;

    private Transform _root;

    public CardFactory(
      IResourceLoader loader,
      CardDataHandler cardsDataHandler,
      SidesDataHandler sidesDataHandler)
    {
      _loader = loader;
      _cardsDataHandler = cardsDataHandler;
      _sidesDataHandler = sidesDataHandler;

      CreateFactoryRoot();
    }

    public CardFacade CreateCard(GameObject instance, CardType type, Transform root = null)
    {
      GameObject prefab = UnityEngine.Object.Instantiate(instance, root);
      CardFacade facade = SetupCard(prefab, type);

      return facade;
    }

    private CardFacade SetupCard(GameObject prefab, CardType type)
    {
      CardData data = _cardsDataHandler.GetCardData(type);
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
        SideStaticData staticData = _sidesDataHandler.GetSideData(data.Sides[i].Type);

        facade.Sides[i].Type = staticData.Type;
        facade.Sides[i].Renderer.sprite = staticData.Icon;
        facade.Sides[i].Value.Set(data.Sides[i].Value);
      }
    }

    private void CreateFactoryRoot() => 
      _root = new GameObject(nameof(CardFactory)).transform;
  }
}