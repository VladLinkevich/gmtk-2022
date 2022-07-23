using System;
using TMPro;
using UnityEngine;

namespace Code.Facade
{
  public class HpBarFacade : MonoBehaviour
  {
    public event Action Destroy;
    
    public Color DeactiveColor;
    public Color ActiveColor;
    public Color HitColor;
    public Color PreviewColor;
    
    public SpriteRenderer[] Hps;
    public SpriteRenderer Shield;
    public TextMeshPro ShildText;

    private int _maxHp;
    private int _current;
    private int _preview;
    private int _shield;

    public int Current => _current;
    
    public void Set(int value)
    {
      _current = value;
      _maxHp = _current;
      UpdateBar();
    }
    

    public void AddToPreview(int value)
    {
      _preview = Mathf.Max(_preview + value, 0);

      UpdateBar();
    }

    public void RemoveToPreview(int value)
    {
      _preview = Mathf.Max(_preview - value, 0);

      UpdateBar();
    }

    public void Hit(int damage)
    {
      damage = Mathf.Max(damage, 0);
      int value = Mathf.Min(_shield, damage);
      
      _shield -= value;
      damage -= value;
      
      damage = Mathf.Max(damage, 0);
      _preview = Mathf.Max(_preview - damage, 0);
      _current -= damage;

      UpdateBar();

      
      if (_current <= 0) 
        Destroy?.Invoke();
    }

    public void AddShield(int shield)
    {
      _shield = shield;
      UpdateBar();
    }

    public void AddHeal(int health)
    {
      _current = Mathf.Max(_current + health, _maxHp);
      UpdateBar();
    }

    private void UpdateBar()
    {
      int preview = _current + _shield - _preview;
      
      for (int i = 0; i < Hps.Length; ++i)
      {
        Color color = DeactiveColor;

        if (i < _maxHp) color = HitColor;
        if (i < _current) color = PreviewColor;
        if (i < _current + _shield - _preview) color = ActiveColor;

        Hps[i].color = color;
      }

      if (_shield == 0) Shield.gameObject.SetActive(false);
      else
      {
        Shield.gameObject.SetActive(true);
        ShildText.text = _shield.ToString();
      }
    }
  }
}