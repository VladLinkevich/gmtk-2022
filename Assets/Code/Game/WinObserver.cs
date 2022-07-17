using System;
using Code.Facade;

namespace Code.Game
{
  public interface IWinObserver
  {
    event Action Win;
    event Action Lose;
  }

  public class WinObserver : IWinObserver
  {
    public event Action Win;
    public event Action Lose;
    
    private readonly IPlayerHandler _playerHandler;
    private readonly IEnemyHandler _enemyHandler;
    private readonly ILoadLevel _loadLevel;

    public WinObserver(
      IPlayerHandler playerHandler,
      IEnemyHandler enemyHandler,
      ILoadLevel loadLevel)
    {
      _playerHandler = playerHandler;
      _enemyHandler = enemyHandler;
      _loadLevel = loadLevel;

      _loadLevel.Complete += Initialize;
    }

    private void Initialize()
    {
      foreach (CardFacade card in _playerHandler.Card) 
        card.DestroyCard += ChangeCard;
      
      foreach (CardFacade card in _enemyHandler.Card) 
        card.DestroyCard += ChangeCard;
    }

    private void ChangeCard(CardFacade card)
    {
      IsWin(card);
      IsLose(card);
    }

    private void IsLose(CardFacade card)
    {
      if (_playerHandler.Card.Contains(card))
      {
        _playerHandler.Card.Remove(card);
        if (_playerHandler.Card.Count == 0) 
          Lose?.Invoke();
      }
    }

    private void IsWin(CardFacade card)
    {
      if (_enemyHandler.Card.Contains(card))
      {
        _enemyHandler.Card.Remove(card);
        if (_enemyHandler.Card.Count == 0) 
          Win?.Invoke();
      }
    }
  }
}