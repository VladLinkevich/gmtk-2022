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

    private readonly ICardDestroyer _cardDestroyer;
    private readonly IPlayerHandler _playerHandler;
    private readonly IEnemyHandler _enemyHandler;

    public WinObserver(
      ICardDestroyer cardDestroyer,
      IPlayerHandler playerHandler,
      IEnemyHandler enemyHandler)
    {
      _cardDestroyer = cardDestroyer;
      _playerHandler = playerHandler;
      _enemyHandler = enemyHandler;
      
      _cardDestroyer.Destroy += Observe;
    }

    private void Observe(CardFacade card)
    {
      if (_playerHandler.Card.Count == 0) 
        Lose?.Invoke();
      
      if (_enemyHandler.Card.Count == 0)
      {
        int level = PlayerPrefs.GetInt("level", 0) + 1;
        PlayerPrefs.SetInt("level", level);
        Win?.Invoke();
      }

    }


    private void IsLose(CardFacade card)
    {
      if (_playerHandler.Card.Contains(card))
      {

        if (_playerHandler.Card.Count == 0) 
          Lose?.Invoke();
      }
    }

    private void IsWin(CardFacade card)
    {
      if (_enemyHandler.Card.Contains(card))
      {

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