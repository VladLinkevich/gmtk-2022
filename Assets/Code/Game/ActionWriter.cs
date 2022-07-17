using System;
using System.Collections.Generic;
using Code.Data;
using Code.Facade;

namespace Code.Game
{
  public interface IActionWriter
  {
    void Write(CardFacade from, CardFacade to);
    void Release();
  }

  public class ActionWriter : IActionWriter
  {
    private readonly Dictionary<Guid, Action> _actions = new Dictionary<Guid, Action>();

    public ActionWriter()
    {
      
    }

    public void Write(CardFacade from, CardFacade to)
    {
      from.DestroyCard += Clear;

      if (((SideAction) from.DiceFacade.Current.Type & SideAction.Attack) == SideAction.Attack)
      {
        to.HpBarFacade.AddToPreview(from.DiceFacade.Current.Value.Get);
        _actions.Add(from.Guid, () => { to.HpBarFacade.Hit(from.DiceFacade.Current.Value.Get);});
      }
    }

    public void Release()
    {
      foreach (Action action in _actions.Values) 
        action.Invoke();
    }

    private void Clear(CardFacade from)
    {
      from.DestroyCard -= Clear;
      
      _actions.Remove(from.Guid);
    }
  }
}