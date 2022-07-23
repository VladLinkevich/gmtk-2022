using System;
using Code.Facade;

namespace Code.Game.CardLogic
{
  public interface ICardHP
  {
  }

  public class CardHP : ICardHP, IDisposable
  {
    private readonly CardFacade _facade;

    public CardHP(CardFacade facade)
    {
      _facade = facade;
      
      _facade.HpBarFacade.Destroy += LoseHp;
    }

    public void Dispose()
    {
      _facade.HpBarFacade.Destroy -= LoseHp;
    }

    private void LoseHp()
    {
      
    }
  }
}