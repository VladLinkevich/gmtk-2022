using Code.Data;
using UnityEngine;

namespace Code.StaticData
{
  [CreateAssetMenu(fileName = "card_data", menuName = "Data/Card", order = 0)]
  public class CardData : ScriptableObject
  {
    public CardType Type;
    public Sprite Character;
    public Color Color;
    
    [Range(1, 5)]
    public int Hp;
  }
}