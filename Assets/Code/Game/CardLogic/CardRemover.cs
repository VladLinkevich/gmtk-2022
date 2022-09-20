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
    private readonly IPlayerHandler _playerHandler;
    private readonly IEnemyHandler _enemyHandler;
    private readonly IPlayerDiceHandler _playerDice;
    private readonly IEnemyDiceHandler _enemyDice;
    public event Action<CardFacade> Destroy; 

    public CardDestroyer(
      IPlayerHandler playerHandler,
      IEnemyHandler enemyHandler,
      IPlayerDiceHandler playerDice,
      IEnemyDiceHandler enemyDice)
    {
      _playerHandler = playerHandler;
      _enemyHandler = enemyHandler;
      _playerDice = playerDice;
      _enemyDice = enemyDice;
    }
    
    private void Initialize()
    {
      foreach (CardFacade card in _playerHandler.Card) 
        card.Destroy += DestroyCard;
      
      foreach (CardFacade card in _enemyHandler.Card) 
        card.Destroy += DestroyCard;
    }

    private void DestroyCard(CardFacade card)
    {
      _playerDice.PlayerDice.Remove(card.DiceFacade);
      _playerHandler.Card.Remove(card);
      
      _enemyDice.EnemyDice.Remove(card.DiceFacade);
      _enemyHandler.Card.Remove(card);
    }
  }
}