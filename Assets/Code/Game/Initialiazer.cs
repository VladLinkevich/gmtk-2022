using Code.StateMachine;
using Zenject;

namespace Code.Game
{
  public class Initialiazer : IInitializable
  {
    private readonly IStateMachine _stateMachine;

    public Initialiazer(
      IStateMachine stateMachine)
    {
      _stateMachine = stateMachine;
    }
    
    public void Initialize() => 
      _stateMachine.ChangeState(typeof(LevelLoader));
  }
}