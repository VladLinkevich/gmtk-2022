using Code.Data;
using UnityEngine;

namespace Code.Game
{
  public class LevelLoader
  {
    private readonly ICardFactory _cardFactory;

    public LevelLoader(ICardFactory cardFactory)
    {
      _cardFactory = cardFactory;

      GameObject archer = _cardFactory.CreatePlayerCard(CardType.Archer);
      GameObject mage = _cardFactory.CreatePlayerCard(CardType.Mage);

      archer.transform.localPosition = Vector3.left;
      mage.transform.localPosition = Vector3.right;
    }
  }
}