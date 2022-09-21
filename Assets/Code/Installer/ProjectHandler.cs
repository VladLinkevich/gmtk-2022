using Code.StaticData;
using UnityEngine;
using Zenject;

namespace Code.Installer
{
  [CreateAssetMenu(fileName = "ProjectHandler", menuName = "Data/ProjectHandler")]
  public class ProjectHandler : ScriptableObjectInstaller<ProjectHandler>
  {
    public CardDataHandler CardDataHandler;
    public SidesDataHandler SidesDataHandler;

    public override void InstallBindings()
    {
      Container.BindInstance(CardDataHandler).IfNotBound();
      Container.BindInstance(SidesDataHandler).IfNotBound();
    }
  }
}