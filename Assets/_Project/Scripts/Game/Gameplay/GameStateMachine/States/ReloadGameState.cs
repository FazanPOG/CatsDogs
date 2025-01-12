using _Project.Game;

namespace _Project.Gameplay
{
    public class ReloadGameState : IGameState
    {
        private readonly ISceneLoaderService _sceneLoaderService;

        public ReloadGameState(ISceneLoaderService sceneLoaderService)
        {
            _sceneLoaderService = sceneLoaderService;
        }
        
        public void Enter()
        {
            _sceneLoaderService.LoadGameplayScene();
        }

        public void Exit() { }
    }
}