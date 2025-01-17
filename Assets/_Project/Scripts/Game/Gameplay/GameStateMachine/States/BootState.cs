using _Project.API;

namespace _Project.Gameplay
{
    public class BootState : IGameState
    {
        private readonly ILevelRewardService _levelRewardService;
        private readonly IAPIEnvironmentService _apiEnvironmentService;

        public BootState(ILevelRewardService levelRewardService, IAPIEnvironmentService apiEnvironmentService)
        {
            _levelRewardService = levelRewardService;
            _apiEnvironmentService = apiEnvironmentService;
        }

        public void Enter()
        {
            _levelRewardService.RandomizeLevelReward();
            _apiEnvironmentService.GameLoadingAndReady();
        }
        
        public void Exit() { }
    }
}