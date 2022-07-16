using Code.StaticData;
using UnityEngine;
using Zenject;

namespace Code.Installer
{
  [CreateAssetMenu(fileName = "ProjectHandler", menuName = "Data/ProjectHandler")]
  public class ProjectHandler : ScriptableObjectInstaller<ProjectHandler>
  {
    public CardHandler CardHandler;
    public SideHandler SideHandler;

    public override void InstallBindings()
    {
      Container.BindInstance(CardHandler).IfNotBound();
      Container.BindInstance(SideHandler).IfNotBound();
    }
  }
}