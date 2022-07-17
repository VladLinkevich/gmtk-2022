using UnityEngine;

namespace Code.Facade
{
  public class EnemyTargetView : MonoBehaviour
  {
    public EnterObserver EnterObserver;
    private ArrowRenderer _arrow;
    private Vector3 _position;

    private void OnEnable()
    {
      EnterObserver.Enter += ShowArrow;
      EnterObserver.Exit += HideArrow;
    }

    private void OnDisable()
    {
      EnterObserver.Enter -= ShowArrow;
      EnterObserver.Exit -= HideArrow;
    }

    public void Setup(
      ArrowRenderer arrow,
      Vector3 position)
    {
      _arrow = arrow;
      _position = position;
    }

    public void Activate()
    {
      if (_arrow != null)
      {
        _arrow.color = true;
        EnterObserver.Ignore = false;
      }
    }

    public void Deactivate()
    {
      if (_arrow) 
        _arrow.color = false;
      
      _arrow = null;
      EnterObserver.Ignore = true;
    }

    private void ShowArrow()
    {
      _arrow.gameObject.SetActive(true);
      _arrow.SetPositions(transform.position, _position);
    }

    private void HideArrow() => 
      _arrow.gameObject.SetActive(false);
  }
}