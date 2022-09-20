using System.Collections.Generic;
using Code.Data;
using Code.Facade;

namespace Code.Game.CardLogic
{
  public interface IDeck
  {
    List<CardFacade> Card { get; }
    void Add(CardType type);
  }

  public class Deck : IDeck
  {
    private readonly ICardFactory _cardFactory;
    
    public List<CardFacade> Card { get; private set; } = new();

    public Deck(ICardFactory cardFactory)
    {
      _cardFactory = cardFactory;
    }

    public void Add(CardType type)
    {
      CardFacade facade = _cardFactory.CreateCard(type);
      
      Card.Add(facade);
    }
  }
}