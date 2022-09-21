using System;
using Code.Data;
using Code.Facade;
using Code.Game;
using Code.Game.CardLogic;
using DG.Tweening;
using UnityEngine;
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
      if (PlayerPrefs.HasKey("tutor_two") == false)
      {
        PlayerPrefs.SetInt("tutor_two", 99);
        GameObject.FindGameObjectWithTag("tutor_two").transform.DOMoveX(0, 0.5f);
      }
      
      _boardFacade.Animator.SetTrigger(Pick);

      foreach (CardFacade card in _player.Card)
      {
        if (((SideAction) card.DiceFacade.Current.Type & (SideAction.Attack | SideAction.Def | SideAction.Use)) != 0)
        {
          card.MouseObserver.Ignore = false;
          ++_usedCard;
          // card.Down += PickCard;
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

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.RaycastNonAlloc(ray, _result, 50, _groundMask) == 1)
          position = _result[0].point;

        _arrow.Player.SetPositions(
          start0: _pickCard.transform.position,
          end0: position);
      }
    }

    private void PickCard(CardFacade card)
    {
     // card.Down -= PickCard;
      //card.Up += ThrowCard;
      _pickCard = card;
      _arrow.Player.gameObject.SetActive(true);
    }

    private void ThrowCard(CardFacade card)
    {
      //card.Up -= ThrowCard;
     // card.Down += PickCard;

      if (((SideAction) card.DiceFacade.Current.Type & (SideAction.Attack | SideAction.Use)) != 0) 
        FindHitCard();

      if (((SideAction) card.DiceFacade.Current.Type & (SideAction.Def)) == SideAction.Def)
        FindPlayerCard();


      _pickCard = null;
      _arrow.Player.gameObject.SetActive(false);
    }

    private void FindHitCard()
    {
      Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
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
      Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
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
//      card.Down -= PickCard;
      card.Character.color = Color.gray;

      --_usedCard;
      if (_usedCard == 0) 
        ChangeState?.Invoke(typeof(RoundEndAction));
    }
  }
}