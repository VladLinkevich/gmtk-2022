using UnityEngine;

namespace Code.Facade
{
  public class ValueFacade : MonoBehaviour
  {
    public GameObject[] Counts;
    private int _value;

    public int Value => _value;
    
    public void Set(int value)
    {
      _value = value;
      SetupCount(value);
    }

    private void SetupCount(int value)
    {
      for (int i = 0, end = Counts.Length; i < end; ++i)
        Counts[i].SetActive(i < value);
    }
  }
}