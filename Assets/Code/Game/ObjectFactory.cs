using System;
using Code.Services.ResourceLoadService;
using UnityEngine;

namespace Code.Game
{
  public interface IArrow
  {
    ArrowRenderer Enemy { get; }
    ArrowRenderer Player { get; }
  }

  public class ObjectFactory : IArrow
  {
    private readonly IResourceLoader _resourceLoader;
    private readonly Settings _settings;
    private readonly ArrowRenderer _enemyArrow;
    private readonly ArrowRenderer _playerArrow;
    private Transform _root;

    ArrowRenderer IArrow.Enemy => _enemyArrow;
    ArrowRenderer IArrow.Player => _playerArrow;

    public ObjectFactory(
      IResourceLoader resourceLoader,
      Settings settings)
    {
      _resourceLoader = resourceLoader;
      _settings = settings;

      CreateRoot();
      _enemyArrow = CreateArrow(_settings.EnemyArrow);
      _playerArrow = CreateArrow(_settings.PlayerArrow);
    }

    private ArrowRenderer CreateArrow(GameObject arrowPrefab)
    {
      GameObject arrow = UnityEngine.Object.Instantiate(arrowPrefab, _root);
      arrow.SetActive(false);
      return arrow.GetComponent<ArrowRenderer>();
    }

    private void CreateRoot() => 
      _root = new GameObject(typeof(ObjectFactory).ToString()).transform;

    [Serializable]
    public class Settings
    {
      public GameObject EnemyArrow;
      public GameObject PlayerArrow;
    }
  }
}