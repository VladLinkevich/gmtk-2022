using System;
using Code.Data;
using Code.Facade;
using Code.Game.CardLogic;
using UnityEngine;
using Zenject;

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
    private readonly IPlayerDeck _player;
    private readonly IEnemyDeck _enemy;

    public WinObserver(
      ICardDestroyer cardDestroyer,
      IPlayerDeck player,
      IEnemyDeck enemy)
    {
      _cardDestroyer = cardDestroyer;
      _player = player;
      _enemy = enemy;
      
      _cardDestroyer.Destroy += Observe;
    }

    private void Observe(CardFacade card)
    {
      if (_player.Card.Count == 0) 
        Lose?.Invoke();
      
      if (_enemy.Card.Count == 0)
      {
        int level = PlayerPrefs.GetInt("level", 0) + 1;
        PlayerPrefs.SetInt("level", level);
        Win?.Invoke();
      }

    }


    private void IsLose(CardFacade card)
    {
      if (_player.Card.Contains(card))
      {

        if (_player.Card.Count == 0) 
          Lose?.Invoke();
      }
    }

    private void IsWin(CardFacade card)
    {
      if (_enemy.Card.Contains(card))
      {

        if (_enemy.Card.Count == 0)
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