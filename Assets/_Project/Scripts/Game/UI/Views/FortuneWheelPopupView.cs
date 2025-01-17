using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.UI
{
    public class FortuneWheelPopupView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _spinText;
        [SerializeField] private TextMeshProUGUI _takeRewardText;
        [SerializeField] private Button _spinButton;
        [SerializeField] private Button _takeRewardButton;
        [SerializeField] private Button _takeADBonusedRewardButton;
        [SerializeField] private Transform _wheelTransform;
        [SerializeField] private List<FortuneWheelRewardView> _fortuneWheelRewardViews;

        public Transform WheelTransform => _wheelTransform;
        public IReadOnlyList<FortuneWheelRewardView> FortuneWheelRewardViews => _fortuneWheelRewardViews;
        
        public event Action OnSpinButtonClicked;
        public event Action OnTakeRewardButtonClicked;
        public event Action OnTakeADBonusedRewardButtonClicked;
        
        private void OnEnable()
        {
            _spinButton.onClick.AddListener(() => OnSpinButtonClicked?.Invoke());
            _takeRewardButton.onClick.AddListener(() => OnTakeRewardButtonClicked?.Invoke());
            _takeADBonusedRewardButton.onClick.AddListener(() => OnTakeADBonusedRewardButtonClicked?.Invoke());
        }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        public void SetSpinText(string text) => _spinText.text = text;
        public void SetTakeRewardText(string text) => _takeRewardText.text = text;
        public void SetSpinButtonActiveState(bool activeState) => _spinButton.gameObject.SetActive(activeState);
        public void SetTakeRewardButtonActiveState(bool activeState) => _takeRewardButton.gameObject.SetActive(activeState);
        public void SetTakeADBonusedRewardButtonActiveState(bool activeState) => _takeADBonusedRewardButton.gameObject.SetActive(activeState);
        
        private void OnDisable()
        {
            _spinButton.onClick.RemoveAllListeners();
            _takeRewardButton.onClick.RemoveAllListeners();
            _takeADBonusedRewardButton.onClick.RemoveAllListeners();
        }
    }
}