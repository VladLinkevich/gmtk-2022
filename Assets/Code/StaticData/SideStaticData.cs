using Code.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.StaticData
{
  [CreateAssetMenu(fileName = "side_data", menuName = "Data/Side", order = 0)]
  public class SideStaticData : ScriptableObject
  {
    public SideType Type;
    public Sprite Icon;
  }
}