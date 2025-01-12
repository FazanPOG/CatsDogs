namespace _Project.Gameplay
{
    public interface ILevelRewardService
    {
        LevelReward Reward { get; }
        void RandomizeLevelReward();
    }
}