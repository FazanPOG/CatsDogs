using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.UI
{
    [CreateAssetMenu(menuName = "_Project/UI/SkinShopConfig")]
    public class SkinShopConfig : ScriptableObject
    {
        [SerializeField, MinValue(0)] private int _unlockSkinPrice;
        [SerializeField, MinValue(0)] private int _adRewardAmount;
        [SerializeField, MinValue(0.1f), MaxValue(5f)] private float _randomSkinUnlockAnimationDuration = 2f;
        [SerializeField, MinValue(0.1f), MaxValue(5f)] private float _randomSkinUnlockAnimationFlashDuration = 0.25f;
        [SerializeField] private SkinConfig[] _skinConfigs;

        public int UnlockSkinPrice => _unlockSkinPrice;

        public int ADRewardAmount => _adRewardAmount;

        public float RandomSkinUnlockAnimationDuration => _randomSkinUnlockAnimationDuration;

        public float RandomSkinUnlockAnimationFlashDuration => _randomSkinUnlockAnimationFlashDuration;
        
        public SkinConfig[] SkinConfigs => _skinConfigs;
    }
}