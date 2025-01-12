namespace _Project.Gameplay
{
    public class BootState : IGameState
    {
        private readonly ILevelRewardService _levelRewardService;

        public BootState(ILevelRewardService levelRewardService)
        {
            _levelRewardService = levelRewardService;
        }

        public void Enter()
        {
            _levelRewardService.RandomizeLevelReward();
        }
        
        public void Exit() { }
    }
}