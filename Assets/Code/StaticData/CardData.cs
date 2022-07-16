using Code.Data;
using Sirenix.OdinInspector;
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
    
    [ValidateInput("D6")]
    public SideData[] Sides;

    private bool D6(SideData[] sides) => 
      sides.Length == 6;
  }
}