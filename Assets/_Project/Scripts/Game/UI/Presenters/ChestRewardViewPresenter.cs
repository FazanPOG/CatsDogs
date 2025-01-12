using System;
using R3;

namespace _Project.UI
{
    public class ChestRewardViewPresenter
    {
        private readonly ChestRewardView _view;
        private readonly ReadOnlyReactiveProperty<int> _availableChestsCount;
        private readonly int _rewardValue;

        private bool _isOpened;

        public event Action<int> OnChestOpened;
        
        public ChestRewardViewPresenter(
            ChestRewardView view, 
            ReadOnlyReactiveProperty<int> availableChestsCount, 
            int rewardValue,
            SpriteReferencesConfig spriteReferencesConfig)
        {
            _view = view;
            _availableChestsCount = availableChestsCount;
            _rewardValue = rewardValue;

            _view.SetRewardValueText($"+{_rewardValue}");
            _view.SetCurrencySprite(spriteReferencesConfig.CurrencySprite);
            
            _view.OnOpenButtonClicked += OnOpenButtonClicked;
            _availableChestsCount.Subscribe(HandleAvailableChestsCount);
        }

        private void OnOpenButtonClicked()
        {
            if(_availableChestsCount.CurrentValue == 0 || _isOpened)
                return;
            
            _view.SetOpenState();
            _isOpened = true;
            
            OnChestOpened?.Invoke(_rewardValue);
        }

        private void HandleAvailableChestsCount(int chestCount)
        {
            if(_isOpened)
                return;
            
            _view.SetCloseState();
            
            if (chestCount > 0)
            {
                _view.SetADImageActiveState(false);
            }
            else
            {
                _view.SetADImageActiveState(true);
            }
        }
    }
}