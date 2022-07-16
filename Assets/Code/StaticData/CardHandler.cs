using System.Collections.Generic;
using System.Linq;
using Code.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.StaticData
{
  [CreateAssetMenu(fileName = "card_handler", menuName = "Data/CardHandler", order = 0)]

  public class CardHandler : ScriptableObject
  {
    public List<CardData> Cards;

    public CardData GetCardData(CardType type) => 
      Cards.FirstOrDefault(x => x.Type == type);
    
    #if UNITY_EDITOR

    [Button]
    public void CollectCard()
    {
      Cards.Clear();

      Cards = UnityEditor.AssetDatabase.FindAssets($"t:{typeof(CardData)}")
        .Select(UnityEditor.AssetDatabase.GUIDToAssetPath)
        .Select(UnityEditor.AssetDatabase.LoadAssetAtPath<CardData>)
        .Where(asset => asset != null)
        .ToList();
    }
    
    #endif
  }
}