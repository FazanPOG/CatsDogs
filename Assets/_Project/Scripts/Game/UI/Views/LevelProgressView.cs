using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.UI
{
    public class LevelProgressView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentLevelText;
        [SerializeField] private Image _barImage;
        [SerializeField] private GameObject _progressBarObject;
        [SerializeField] private bool _showBar;

        public bool ShowBar => _showBar;

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
        
        public void SetProgressBarActiveState(bool activeState) => _progressBarObject.gameObject.SetActive(activeState);
        public void SetCurrentLevelText(string text) => _currentLevelText.text = text;
        public void SetProgressBarFillAmount(float value) => _barImage.fillAmount = value;
    }
}