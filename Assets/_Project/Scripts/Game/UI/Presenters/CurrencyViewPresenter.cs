using _Project.Data;
using _Project.Gameplay;
using DG.Tweening;
using R3;
using UnityEngine;

namespace _Project.UI
{
    public class CurrencyViewPresenter
    {
        private const float ANIMATION_DURATION = 1.5f;
            
        private readonly CurrencyView _view;

        private int _previousCurrencyValue;

        private Coroutine _coroutine;
        
        public CurrencyViewPresenter(
            CurrencyView view, 
            ReadOnlyReactiveProperty<int> money, 
            SpriteReferencesConfig referencesConfig,
            IGameStateProvider gameStateProvider)
        {
            _view = view;
            _previousCurrencyValue = money.CurrentValue;
            
            _view.SetCurrencySprite(referencesConfig.CurrencySprite);
            UpdateViewText(money.CurrentValue);
            money.Skip(1).Subscribe(OnMoneyChanged);
            gameStateProvider.GameState.Subscribe(HandleGameState);
        }

        private void HandleGameState(IGameState gameState)
        {
            if (gameState is BootState)
                _view.Show();
            else
                _view.Hide();
        }
        
        private void OnMoneyChanged(int money)
        {
            AnimateCurrency(_previousCurrencyValue, money, ANIMATION_DURATION);
            _previousCurrencyValue = money;
        }
        
        private void UpdateViewText(int money) => _view.SetText(money.ToString());

        private void AnimateCurrency(int startValue, int endValue, float duration)
        {
            int currentValue = startValue;
            int diff = endValue - startValue;

            if (diff > 0)
                _view.SetAddCurrencyColor();
            else
                _view.SetSpendCurrencyColor();

            DOTween.To(() => currentValue, x =>
                {
                    currentValue = x;
                    _view.SetText(currentValue.ToString());
                }, endValue, duration)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    UpdateViewText(endValue);
                    _view.SetDefaultCurrencyColor();
                });
        }
    }
}