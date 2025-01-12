using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.UI
{
    public class LevelProgressView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentLevelText;
        [SerializeField] private Image _barImage;

        public void SetCurrentLevelText(string text) => _currentLevelText.text = text;
        public void SetProgressBarFillAmount(float value) => _barImage.fillAmount = value;
    }
}