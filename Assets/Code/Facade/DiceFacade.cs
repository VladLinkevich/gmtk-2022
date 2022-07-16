using System;
using InnerDriveStudios.DiceCreator;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Facade
{
  public class DiceFacade : MonoBehaviour
  {
    [ReadOnly]
    public Die Die;
    public MeshRenderer Renderer;
    public Rigidbody Rigidbody;
    public MeshCollider Collider;
    
    [ValidateInput("@Sides.Length == 6")]
    public SideFacade[] Sides;


    public void TogglePhysic(bool flag)
    {
      Rigidbody.velocity = Vector3.zero;
      
      Rigidbody.useGravity = flag;
      Collider.isTrigger = !flag;
    }

#if UNITY_EDITOR

    private void OnValidate() => 
      Die = GetComponent<Die>();

#endif
  }
}