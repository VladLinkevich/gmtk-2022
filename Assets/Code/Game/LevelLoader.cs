using Code.Data;

namespace Code.Game
{
  public class LevelLoader
  {
    private readonly ICardFactory _cardFactory;

    public LevelLoader(ICardFactory cardFactory)
    {
      _cardFactory = cardFactory;

      _cardFactory.CreatePlayerCard(CardType.Archer);
    }
  }
}