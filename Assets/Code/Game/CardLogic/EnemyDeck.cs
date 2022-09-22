using System;
using System.Collections.Generic;
using Code.Facade;

namespace Code.Game.CardLogic
{
  public interface IEnemyDeck : IDeck
  {
    
  }
  
  public class EnemyDeck : Deck, IEnemyDeck
  {
    public List<EnemyCardFacade> Card;
    
    public EnemyDeck(
      ICardFactory cardFactory,
      ICardPositioner cardPositioner,
      Settings settings) :
      base(cardFactory, cardPositioner, settings)
    {
    }
    

    [Serializable]
    public class Settings : Deck.Settings
    {
    }
  }
}