using System;
using Code.Data;
using Code.Facade;
using Code.Game;
using Code.Game.CardLogic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Code.StateMachine
{
  public class PlayerPick : IState, ITickable
  {
    private readonly IPlayerDeck _player;
    private readonly IEnemyDeck _enemy;
    private readonly IArrow _arrow;
    private readonly IWinObserver _winObserver;
    private readonly BoardFacade _boardFacade;
    private readonly Camera _camera;
    private readonly RaycastHit[] _result = new RaycastHit[1];
    private readonly int _cardMask;
    private readonly int _groundMask;
    
    private CardFacade _pickCard;
    private static readonly int Hide = Animator.StringToHash("hide");
    private static readonly int Pick = Animator.StringToHash("pick");
    private int _usedCard;
    
    private Vector3 _startPosition;
    private Vector3 _nearEnemy;

    public event Action<Type> ChangeState;

    public PlayerPick(
      IPlayerDeck player,
      IEnemyDeck enemy,
      IArrow arrow,
      IWinObserver winObserver,
      BoardFacade boardFacade)
    {
      _player = player;
      _enemy = enemy;
      _arrow = arrow;
      _winObserver = winObserver;
      _boardFacade = boardFacade;

      _camera = Camera.main;
      
      _groundMask = LayerMask.GetMask("Mouse");
      _cardMask = LayerMask.GetMask("Card");
    }

    public void Enter()
    {
      _boardFacade.Animator.SetTrigger(Pick);

      foreach (CardFacade card in _player.Card)
      {
        if (((SideAction) card.DiceFacade.Current.Type & (SideAction.Attack | SideAction.Def | SideAction.Use)) != 0)
        {
          card.MouseObserver.Ignore = false;
          ++_usedCard;
          card.MouseObserver.Down += PickCard;
        }
      }

      _winObserver.Lose += CompleteLevel;
      _winObserver.Win += CompleteLevel;
      _boardFacade.Done.Observer.Click += EndRound;
    }

    private void EndRound() => 
      ChangeState?.Invoke(typeof(RoundEndAction));

    public void Exit()
    {
      _boardFacade.Animator.SetTrigger(Hide);
      _boardFacade.Done.Observer.Click -= EndRound;
      _winObserver.Lose -= CompleteLevel;
      _winObserver.Win -= CompleteLevel;
      
      foreach (CardFacade card in _player.Card) 
        card.Character.color = Color.white;
    }

    private void CompleteLevel() => 
      ChangeState?.Invoke(typeof(WinState));

    public void Tick()
    {
      if (_pickCard != null)
      {
        Vector3 position = Vector3.zero;

        Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.RaycastNonAlloc(ray, _result, 50, _groundMask) == 1)
          position = _result[0].point;

        _arrow.Player.SetPositions(
          start0: _pickCard.transform.position,
          end0: _nearEnemy + position - _startPosition);
      }
    }

    private void PickCard(CardFacade card)
    {
      card.MouseObserver.Down -= PickCard;
      card.MouseObserver.Up += ThrowCard;
      
      _pickCard = card;
      _arrow.Player.gameObject.SetActive(true);

      Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
      if (Physics.RaycastNonAlloc(ray, _result, 50, _groundMask) == 1)
        _startPosition = _result[0].point;
      _nearEnemy = FindNearEnemy(card.Position);
      
      _arrow.Player.SetPositions(
        start0: _pickCard.transform.position,
        end0: _nearEnemy);
    }

    private void ThrowCard(CardFacade card)
    {
      card.MouseObserver.Up -= ThrowCard;
      card.MouseObserver.Down += PickCard;

      if (((SideAction) card.DiceFacade.Current.Type & (SideAction.Attack | SideAction.Use)) != 0) 
        FindHitCard();

      if (((SideAction) card.DiceFacade.Current.Type & (SideAction.Def)) == SideAction.Def)
        FindPlayerCard();


      _pickCard = null;
      _arrow.Player.gameObject.SetActive(false);
    }

    private void FindHitCard()
    {
      Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
      if (Physics.RaycastNonAlloc(ray, _result, 50, _cardMask) == 1)
      {
        CardFacade card = _result[0].transform.gameObject.GetComponentInParent<CardFacade>();
        if (_enemy.Card.Contains(card))
        {
          card.HpBarFacade.Hit(_pickCard.DiceFacade.Current.Value.Get);
          UseCard(_pickCard);
        }
      }
    }

    private void FindPlayerCard()
    {
      Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
      if (Physics.RaycastNonAlloc(ray, _result, 50, _cardMask) == 1)
      {
        CardFacade card = _result[0].transform.gameObject.GetComponentInParent<CardFacade>();
        if (_player.Card.Contains(card))
        {
          if (_pickCard.DiceFacade.Current.Type == SideType.Shield)
            card.HpBarFacade.AddShield(_pickCard.DiceFacade.Current.Value.Get);
          
          if (_pickCard.DiceFacade.Current.Type == SideType.Life)
            card.HpBarFacade.AddHeal(_pickCard.DiceFacade.Current.Value.Get);

          UseCard(_pickCard);
        }
      }
    }

    private void UseCard(CardFacade card)
    {
      card.MouseObserver.Ignore = true;
      card.MouseObserver.Down -= PickCard;
      card.Character.color = Color.gray;

      --_usedCard;
      if (_usedCard == 0) 
        ChangeState?.Invoke(typeof(RoundEndAction));
    }

    private Vector3 FindNearEnemy(Vector3 position)
    {
      Vector3 near = position;
      float minDistance = Single.MaxValue;

      for (int i = 0, end = _enemy.Card.Count; i < end; ++i)
      {
        float distance = Vector3.Distance(_enemy.Card[i].Position, position);
        if (minDistance > distance)
        {
          minDistance = distance;
          near = _enemy.Card[i].Position;
        }
      }

      return near;
    }
  }
}