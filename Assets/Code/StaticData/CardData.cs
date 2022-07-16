using Code.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.StaticData
{
  [CreateAssetMenu(fileName = "card_data", menuName = "Data/Card", order = 0)]
  public class CardData : ScriptableObject
  {
    [OnValueChanged("Rename")]
    public CardType Type;
    public Sprite Character;
    public Color Color;
    
    [Range(1, 5)]
    public int Hp;
    
    [ValidateInput("@Sides.Length == 6")]
    public SideData[] Sides;

#if UNITY_EDITOR
    
    public void Rename()
    {
      string assetPath = UnityEditor.AssetDatabase.GetAssetPath(this.GetInstanceID());
      UnityEditor.AssetDatabase.RenameAsset(assetPath, Type.ToString().ToLower());
    }

#endif
  }
}