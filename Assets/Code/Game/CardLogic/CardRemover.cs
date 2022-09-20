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

    private readonly IEnemyHandler _enemyHandler;

    private readonly IEnemyDiceHandler _enemyDice;
    public event Action<CardFacade> Destroy; 

    public CardDestroyer(

      IEnemyHandler enemyHandler,

      IEnemyDiceHandler enemyDice)
    {

      _enemyHandler = enemyHandler;

      _enemyDice = enemyDice;
    }
    
    private void Initialize()
    {
      foreach (CardFacade card in _enemyHandler.Card) 
        card.Destroy += DestroyCard;
    }

    private void DestroyCard(CardFacade card)
    {

      
      _enemyDice.EnemyDice.Remove(card.DiceFacade);
      _enemyHandler.Card.Remove(card);
    }
  }
}