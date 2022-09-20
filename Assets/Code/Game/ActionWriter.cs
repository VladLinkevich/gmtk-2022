using System.Collections.Generic;
using Code.Data;
using Code.Facade;
using Code.Game.CardLogic;

namespace Code.Game
{
  public interface IActionWriter
  {
    void Write(CardFacade from, CardFacade to);
    void Release();
  }

  public class ActionWriter : IActionWriter
  {
    private readonly IEnemyHandler _enemyHandler;
    private readonly Dictionary<CardFacade, CardFacade> _actions = new Dictionary<CardFacade, CardFacade>();


    public ActionWriter(
      IEnemyHandler enemyHandler)
    {
      _enemyHandler = enemyHandler;
    }

    public void Write(CardFacade from, CardFacade to)
    {
      from.Destroy += Clear;

      if (((SideAction) from.DiceFacade.Current.Type & SideAction.Attack) == SideAction.Attack)
      {
        to.HpBarFacade.AddToPreview(from.DiceFacade.Current.Value.Get);
        _actions.Add(from, to);
      }
    }

    public void Release()
    {
      foreach (var action in _actions) 
        action.Value.HpBarFacade.Hit(action.Key.DiceFacade.Current.Value.Get);
      
      _actions.Clear();
    }

    private void Clear(CardFacade from)
    {
      from.Destroy -= Clear;
      
      if (_actions.ContainsKey(from))
      {
        _actions[from].HpBarFacade.RemoveToPreview(from.DiceFacade.Current.Value.Get);
        _actions.Remove(from);
      }
    }

    private void Subscribe()
    {
      foreach (CardFacade card in _enemyHandler.Card) 
        card.Destroy += Clear;
    }
  }
}