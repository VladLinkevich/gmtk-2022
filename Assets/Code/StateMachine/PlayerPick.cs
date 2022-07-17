using System;
using Code.Data;
using Code.Facade;
using Code.Game;
using UnityEngine;
using Zenject;

namespace Code.StateMachine
{
  public class PlayerPick : IState, ITickable
  {
    private readonly IPlayerHandler _playerHandler;
    private readonly IEnemyHandler _enemyHandler;
    private readonly IArrow _arrow;
    private readonly IWinObserver _winObserver;
    private readonly BoardFacade _boardFacade;
    private readonly Camera _camera;
    private readonly RaycastHit[] _result = new RaycastHit[1];
    private readonly int _cardMask;
    private readonly int _groundMask;
    
    private CardFacade _pickCard;
    private static readonly int Hide = Animator.StringToHash("Hide");
    private static readonly int Pick = Animator.StringToHash("pick");

    public event Action<Type> ChangeState;

    public PlayerPick(
      IPlayerHandler playerHandler,
      IEnemyHandler enemyHandler,
      IArrow arrow,
      IWinObserver winObserver,
      BoardFacade boardFacade)
    {
      _playerHandler = playerHandler;
      _enemyHandler = enemyHandler;
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

      foreach (CardFacade card in _playerHandler.Card)
      {
        if (((SideAction) card.DiceFacade.Current.Type & (SideAction.Attack | SideAction.Def | SideAction.Use)) != 0)
        {
          card.MouseObserver.Ignore = false;
          card.Down += PickCard;
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
      
      foreach (CardFacade card in _playerHandler.Card) 
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
      card.Down -= PickCard;
      card.Up += ThrowCard;
      _pickCard = card;
      _arrow.Player.gameObject.SetActive(true);
    }

    private void ThrowCard(CardFacade card)
    {
      card.Up -= ThrowCard;
      card.Down += PickCard;

      if (((SideAction) card.DiceFacade.Current.Type & (SideAction.Attack | SideAction.Use)) != 0) 
        FindHitCard();

      _pickCard = null;
      _arrow.Player.gameObject.SetActive(false);
    }

    private void FindHitCard()
    {
      Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
      if (Physics.RaycastNonAlloc(ray, _result, 50, _cardMask) == 1)
      {
        CardFacade card = _result[0].transform.gameObject.GetComponentInParent<CardFacade>();
        if (_enemyHandler.Card.Contains(card))
        {
          card.HpBarFacade.Hit(_pickCard.DiceFacade.Current.Value.Get);
          UseCard(_pickCard);
        }
      }
    }

    private void UseCard(CardFacade card)
    {
      card.MouseObserver.Ignore = true;
      card.Down -= PickCard;
      card.Character.color = Color.gray;
    }
  }
}