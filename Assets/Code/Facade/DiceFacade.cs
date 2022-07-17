using System;
using Code.Data;
using InnerDriveStudios.DiceCreator;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Facade
{
  public class DiceFacade : MonoBehaviour
  {
    [ReadOnly]
    public CardType Type;

    [ReadOnly]
    public Die Die;

    [ReadOnly]
    public DieSides DieSides;

    public MeshRenderer Renderer;
    public Rigidbody Rigidbody;
    public MeshCollider Collider;
    
    [ValidateInput("@Sides.Length == 6")]
    public SideFacade[] Sides;

    public SideFacade Current => Sides[Value];
    public int Value { get; private set; }

    private void OnEnable()
    {
      Die.OnRollEnd += UpdateSkill;
    }

    public void TogglePhysic(bool flag)
    {
      Rigidbody.velocity = Vector3.zero;
      
      Rigidbody.useGravity = flag;
      Collider.isTrigger = !flag;
    }

    private void UpdateSkill(ARollable rollable)
    {
      DieSideMatchInfo info = DieSides.GetDieSideMatchInfo();
      Debug.Log(info.closestMatch.values[0]);
      Value = info.closestMatch.values[0];
    }


#if UNITY_EDITOR

    private void OnValidate()
    {
      Die = GetComponent<Die>();
      DieSides = GetComponent<DieSides>();
    }

#endif
  }
}