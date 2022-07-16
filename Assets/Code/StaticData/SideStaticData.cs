using Code.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.StaticData
{
  [CreateAssetMenu(fileName = "side_data", menuName = "Data/Side", order = 0)]
  public class SideStaticData : ScriptableObject
  {
    [ValidateInput("Rename")]
    public SideType Type;
    public Sprite Icon;
    
#if UNITY_EDITOR
    
    public void Rename()
    {
      string assetPath = UnityEditor.AssetDatabase.GetAssetPath(this.GetInstanceID());
      UnityEditor.AssetDatabase.RenameAsset(assetPath, Type.ToString().ToLower());
    }

#endif
  }
}