using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.UI
{
    [CreateAssetMenu(menuName = "_Project/UI/FortuneWheelRewardConfig")]
    public class FortuneWheelRewardConfig : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField, MinValue(0)] private int _rewardValue;

        public string ID => _id;
        public int RewardValue => _rewardValue;
    }
}