using System;
using UnityEngine;

namespace Code.Facade
{
  public class HpBarFacade : MonoBehaviour
  {
    public Color DeactiveColor;
    public Color ActiveColor;
    public Color HitColor;
    public Color PreviewColor;
    
    public SpriteRenderer[] Hps;

    private int _maxHp;
    private int _current;
    private int _preview;

    public int Current => _current;
    
    public void Set(int value)
    {
      _maxHp = _current;
      _current = value;
      SetupCount(value);
    }

    private void SetupCount(int value)
    {
      for (int i = 0, end = Hps.Length; i < end; ++i)
        Hps[i].color = (i < value) ? ActiveColor : DeactiveColor;
    }

    public void AddToPreview(int value)
    {
      _preview = Mathf.Max(_preview + value, 0);
      
      for (int i = _current - 1, end = Mathf.Max(_current - _preview, 0); i >= end; --i) 
        Hps[i].color = PreviewColor;
    }

    public void Hit(int value)
    {
      value = Mathf.Max(value, 0);
      _preview = Mathf.Max(_preview - value, 0);
      
      for (int i = _current - 1, end = Mathf.Max(_current - value, 0); i >= end; --i) 
        Hps[i].color = PreviewColor;
      
      if (_current - value <= 0) 
        Destroy(this);
    }
  }
}