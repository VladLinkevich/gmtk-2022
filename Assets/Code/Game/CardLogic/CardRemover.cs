using System;
using Code.Facade;


namespace Code.Game.CardLogic
{
  public interface ICardDestroyer
  {
    public event Action<CardFacade> Destroy;
  }

  public class CardDestroyer : ICardDestroyer
  {
    
    public event Action<CardFacade> Destroy; 

    public CardDestroyer()
    {
    }
    
    private void Initialize()
    {
    }

    private void DestroyCard(CardFacade card)
    {
    }
  }
}