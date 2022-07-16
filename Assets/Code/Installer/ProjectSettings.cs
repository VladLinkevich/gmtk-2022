using Code.Game;
using UnityEngine;
using Zenject;

namespace Code.Installer
{
  [CreateAssetMenu(fileName = "ProjectSettings", menuName = "Data/ProjectSettings")]
  public class ProjectSettings : ScriptableObjectInstaller<ProjectSettings>
  {
    public CardFactory.Settings CardFactory;
    public CardPositioner.Settings CardPositioner;

    public override void InstallBindings()
    {
      Container.BindInstance(CardFactory).IfNotBound();
      Container.BindInstance(CardPositioner).IfNotBound();
    }
  }
}