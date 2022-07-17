using System;
using Code.Game;
using Code.Services.SceneLoadService;

namespace Code.StateMachine
{
  public class WinState : IState
  {
    private readonly ISceneLoader _sceneLoader;
    private readonly LevelLoader _levelLoader;
    
    public event Action<Type> ChangeState;

    public WinState(ISceneLoader sceneLoader)
    {
      _sceneLoader = sceneLoader;
    }

    public void Enter()
    {

      _sceneLoader.Load("Game");
    }

    public void Exit()
    {
      throw new NotImplementedException();
    }
  }
}