using System;

namespace Code.Game.CardLogic
{
  
  public interface IPlayerDeck : IDeck
  {
  }
  
  public class PlayerDeck : Deck, IPlayerDeck
  {
    public PlayerDeck(
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