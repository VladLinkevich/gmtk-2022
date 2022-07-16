using System;
using Code.Services.ResourceLoadService;
using UnityEngine;

namespace Code.Game
{
  public interface IArrow
  {
    ArrowRenderer Prefab { get; }
  }

  public class ObjectFactory : IArrow
  {
    private readonly IResourceLoader _resourceLoader;
    private readonly Settings _settings;
    private readonly ArrowRenderer _arrow;
    private Transform _root;

    ArrowRenderer IArrow.Prefab => _arrow;

    public ObjectFactory(
      IResourceLoader resourceLoader,
      Settings settings)
    {
      _resourceLoader = resourceLoader;
      _settings = settings;

      CreateRoot();
      _arrow = CreateArrow();
    }

    private ArrowRenderer CreateArrow()
    {
      GameObject arrow = UnityEngine.Object.Instantiate(_settings.Arrow, _root);
      arrow.SetActive(false);
      return arrow.GetComponent<ArrowRenderer>();
    }

    private void CreateRoot() => 
      _root = new GameObject(typeof(ObjectFactory).ToString()).transform;

    [Serializable]
    public class Settings
    {
      public GameObject Arrow;
    }
  }
}