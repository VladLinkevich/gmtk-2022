using System;

namespace Code.Game.CardLogic
{
  public interface IEnemyDeck : IDeck
  {
  }
  
  public class EnemyDeck : Deck, IEnemyDeck
  {
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