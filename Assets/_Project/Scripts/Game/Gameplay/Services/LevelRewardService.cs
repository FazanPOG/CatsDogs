using System;
using Random = UnityEngine.Random;

namespace _Project.Gameplay
{
    public class LevelRewardService : ILevelRewardService
    {
        public LevelReward Reward { get; private set; }
        
        public void RandomizeLevelReward()
        {
            var rewards = Enum.GetValues(typeof(LevelReward));
    
            var randomIndex = Random.Range(0, rewards.Length);
    
            Reward = (LevelReward)rewards.GetValue(randomIndex);
        }
    }
}