using System;
using Code.Facade;
using Code.Game.CardLogic;
using UnityEngine;

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
    private readonly IPlayerDiceHandler _playerDice;
    private readonly IEnemyDiceHandler _enemyDice;
    private readonly ILoadLevel _loadLevel;

    public WinObserver(
      IPlayerHandler playerHandler,
      IEnemyHandler enemyHandler,
      IPlayerDiceHandler playerDice,
      IEnemyDiceHandler enemyDice,
      ILoadLevel loadLevel)
    {
      _playerHandler = playerHandler;
      _enemyHandler = enemyHandler;
      _playerDice = playerDice;
      _enemyDice = enemyDice;
      _loadLevel = loadLevel;

      _loadLevel.Complete += Initialize;
    }

    private void Initialize()
    {
      foreach (CardFacade card in _playerHandler.Card) 
        card.Destroy += ChangeCard;
      
      foreach (CardFacade card in _enemyHandler.Card) 
        card.Destroy += ChangeCard;
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
        _playerDice.PlayerDice.Remove(card.DiceFacade);
        _playerHandler.Card.Remove(card);
        if (_playerHandler.Card.Count == 0) 
          Lose?.Invoke();
      }
    }

    private void IsWin(CardFacade card)
    {
      if (_enemyHandler.Card.Contains(card))
      {
        _enemyDice.EnemyDice.Remove(card.DiceFacade);
        _enemyHandler.Card.Remove(card);
        if (_enemyHandler.Card.Count == 0)
        {
          Debug.Log("Win");
          int level = PlayerPrefs.GetInt("level", 0) + 1;
          PlayerPrefs.SetInt("level", level);
          Win?.Invoke();
        }
      }
    }
  }
}