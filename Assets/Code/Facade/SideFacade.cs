using Code.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Facade
{
  public class SideFacade : MonoBehaviour
  {
    [ReadOnly]
    public SideType Type;
    
    public SpriteRenderer Renderer;
    public ValueFacade Value;
  }
}