using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Facade
{
  public class DiceFacade : MonoBehaviour
  {
    public MeshRenderer Renderer;
    
    [ValidateInput("@Sides.Length == 6")]
    public SideFacade[] Sides;
    
  }
}