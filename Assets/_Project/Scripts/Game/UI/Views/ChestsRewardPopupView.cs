using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.UI
{
    public class ChestsRewardPopupView : MonoBehaviour
    {
        [SerializeField] private Button _takeRewardButton;
        [SerializeField] private Button _watchADButton;
        [SerializeField] private ChestRewardView[] _chestRewardViews;

        public ChestRewardView[] ChestRewardViews => _chestRewardViews;

        public event Action OnTakeRewardButtonClicked;
        public event Action OnWatchADButtonClicked;
        
        private void OnEnable()
        {
            _takeRewardButton.onClick.AddListener(() => OnTakeRewardButtonClicked?.Invoke());
            _watchADButton.onClick.AddListener(() => OnWatchADButtonClicked?.Invoke());
        }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        public void SetTakeRewardButtonActiveState(bool activeState) => _takeRewardButton.gameObject.SetActive(activeState);
        public void SetWatchADButtonActiveState(bool activeState) => _watchADButton.gameObject.SetActive(activeState);
        
        private void OnDisable()
        {
            _takeRewardButton.onClick.RemoveAllListeners();
            _watchADButton.onClick.RemoveAllListeners();
        }
    }
}