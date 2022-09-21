using System.Collections.Generic;
using System.Linq;
using Code.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.StaticData
{
  [CreateAssetMenu(fileName = "side_handler", menuName = "Data/SideHandler", order = 0)]

  public class SidesDataHandler : ScriptableObject
  {
    public List<SideStaticData> Sides;

    public SideStaticData GetSideData(SideType type) => 
      Sides.FirstOrDefault(side => side.Type == type);

#if UNITY_EDITOR

    [Button]
    public void CollectCard()
    {
      Sides.Clear();

      Sides = UnityEditor.AssetDatabase.FindAssets($"t:{typeof(SideStaticData)}")
        .Select(UnityEditor.AssetDatabase.GUIDToAssetPath)
        .Select(UnityEditor.AssetDatabase.LoadAssetAtPath<SideStaticData>)
        .Where(asset => asset != null)
        .ToList();
    }
    
#endif
  }
}