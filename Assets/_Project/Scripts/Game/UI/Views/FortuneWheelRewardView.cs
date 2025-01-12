using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.UI
{
    public class FortuneWheelRewardView : MonoBehaviour
    {
        [SerializeField] private Image _rewardImage;
        [SerializeField] private TextMeshProUGUI _rewardValueText;

        public void SetRewardSprite(Sprite sprite) => _rewardImage.sprite = sprite;
        public void SetRewardText(string text) => _rewardValueText.text = text;
    }
}