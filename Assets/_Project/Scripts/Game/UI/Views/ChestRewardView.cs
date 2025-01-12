using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.UI
{
    public class ChestRewardView : MonoBehaviour
    {
        [SerializeField] private GameObject _openStateObject;
        [SerializeField] private GameObject _closeStateObject;
        [SerializeField] private Image _adImage;
        [SerializeField] private Image _currencyImage;
        [SerializeField] private TextMeshProUGUI _rewardValueText;
        [SerializeField] private Button _openButton;

        public event Action OnOpenButtonClicked;
        
        private void OnEnable()
        {
            _openButton.onClick.AddListener(() => OnOpenButtonClicked?.Invoke());
        }

        public void SetOpenState()
        {
            _openStateObject.SetActive(true);
            _closeStateObject.SetActive(false);
        }
        
        public void SetCloseState()
        {
            _closeStateObject.SetActive(true);
            _openStateObject.SetActive(false);
        }

        public void SetADImageActiveState(bool activeState) => _adImage.gameObject.SetActive(activeState);
        
        public void SetCurrencySprite(Sprite sprite) => _currencyImage.sprite = sprite;
        public void SetRewardValueText(string text) => _rewardValueText.text = text;

        private void OnDisable()
        {
            _openButton.onClick.RemoveAllListeners();
        }
    }
}