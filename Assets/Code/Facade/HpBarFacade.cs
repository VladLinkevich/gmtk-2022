using UnityEngine;

namespace Code.Facade
{
  public class HpBarFacade : MonoBehaviour
  {
    public GameObject[] Hps;
    private int _current;

    public int Current => _current;
    
    public void Set(int value)
    {
      _current = value;
      SetupCount(value);
    }

    private void SetupCount(int value)
    {
      for (int i = 0, end = Hps.Length; i < end; ++i)
        Hps[i].SetActive(i < value);
    }
  }
}