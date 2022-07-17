using System;
using Code.Data;
using InnerDriveStudios.DiceCreator;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Facade
{
  public class DiceFacade : MonoBehaviour
  {
    public event Action<DiceFacade> Click;
    
    [ReadOnly]
    public CardType Type;

    [ReadOnly]
    public Die Die;

    [ReadOnly]
    public DieSides DieSides;

    [ReadOnly] 
    public ObserveOnClick Observe;

    public MeshRenderer Renderer;
    public Rigidbody Rigidbody;
    public MeshCollider Collider;
    
    [ValidateInput("@Sides.Length == 6")]
    public SideFacade[] Sides;

    public SideFacade Current => Sides[Value];
    public int Value { get; private set; }
    
    public Vector3 SaveRotation { get; private set; }
    public Vector3 SavePosition { get; private set; }

    private void OnEnable()
    {
      Die.OnRollEnd += UpdateSkill;
      Observe.Click += OnClick;
    }

    private void OnDisable()
    {
      Die.OnRollEnd -= UpdateSkill;
      Observe.Click -= OnClick;
    }

    public void ActivatePhysic(bool flag)
    {
      Rigidbody.velocity = Vector3.zero;
      
      Rigidbody.useGravity = flag;
      Collider.isTrigger = !flag;
    }

    private void UpdateSkill(ARollable rollable)
    {
      DieSideMatchInfo info = DieSides.GetDieSideMatchInfo();
      Value = info.closestMatch.values[0];
    }

    private void OnClick() => 
      Click?.Invoke(this);

    public void SavePositionAndRotation()
    {
      SaveRotation = transform.eulerAngles;
      SavePosition = transform.position;
    }

#if UNITY_EDITOR

    private void OnValidate()
    {
      Die = GetComponent<Die>();
      DieSides = GetComponent<DieSides>();
      Observe = GetComponent<ObserveOnClick>();
    }

#endif
  }
}