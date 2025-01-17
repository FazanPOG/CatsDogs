using _Project.API;
using _Project.Game;

namespace _Project.Gameplay
{
    public class ReloadGameState : IGameState
    {
        private readonly ISceneLoaderService _sceneLoaderService;
        private readonly IADService _adService;

        public ReloadGameState(ISceneLoaderService sceneLoaderService, IADService adService)
        {
            _sceneLoaderService = sceneLoaderService;
            _adService = adService;
        }

        public void Enter()
        {
            _adService.ShowFullscreen();
            LoadNextLevel();
        }

        private void LoadNextLevel()
        {
            _sceneLoaderService.LoadGameplayScene();
        }
        
        public void Exit() { }
    }
}