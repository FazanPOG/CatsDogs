using System;
using _Project.API;
using _Project.Data;
using _Project.Gameplay;
using R3;
using Random = UnityEngine.Random;

namespace _Project.UI
{
    public class ChestsRewardPopupViewPresenter
    {
        private const string CHEST_REWARD_AD_KEY = nameof(CHEST_REWARD_AD_KEY);
        private const int DEFAULT_UNLOCK_CHESTS_COUNT = 3;
        private const int AD_WATCH_REWARD_UNLOCK_CHESTS_COUNT = 3;
        private const int MAX_CHEST_COUNT = 9;
        
        private readonly ChestsRewardPopupView _chestsRewardPopupView;
        private readonly SpriteReferencesConfig _spriteReferencesConfig;
        private readonly IGameplayDataProvider _gameplayDataProvider;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IGameStateProvider _gameStateProvider;
        private readonly ILevelRewardService _levelRewardService;
        private readonly IADService _adService;
        private readonly ReactiveProperty<int> _availableChestCount = new ReactiveProperty<int>();

        private int _reward;
        private int _openChestsCount;
        
        public ChestsRewardPopupViewPresenter(
            ChestsRewardPopupView chestsRewardPopupView,
            SpriteReferencesConfig spriteReferencesConfig,
            IGameplayDataProvider gameplayDataProvider,
            IGameStateMachine gameStateMachine,
            IGameStateProvider gameStateProvider,
            ILevelRewardService levelRewardService,
            IADService adService)
        {
            _chestsRewardPopupView = chestsRewardPopupView;
            _spriteReferencesConfig = spriteReferencesConfig;
            _gameplayDataProvider = gameplayDataProvider;
            _gameStateMachine = gameStateMachine;
            _gameStateProvider = gameStateProvider;
            _levelRewardService = levelRewardService;
            _adService = adService;

            Init();
        }

        private void Init()
        {
            _availableChestCount.Value = DEFAULT_UNLOCK_CHESTS_COUNT;
            
            _chestsRewardPopupView.SetTakeRewardButtonActiveState(false);
            _chestsRewardPopupView.SetWatchADButtonActiveState(false);
            
            foreach (var chestRewardView in _chestsRewardPopupView.ChestRewardViews)
            {
                var chestPresenter = new ChestRewardViewPresenter(chestRewardView, _availableChestCount, GetRandomReward(), _spriteReferencesConfig);
                chestPresenter.OnChestOpened += OnChestOpened;
            }
            
            _chestsRewardPopupView.OnTakeRewardButtonClicked += TakeReward;
            _chestsRewardPopupView.OnWatchADButtonClicked += WatchAD;
            _adService.OnRewardedReward += OnRewardedReward;
            _gameStateProvider.GameState.Subscribe(HandleGameState);
            
            _chestsRewardPopupView.Hide();
        }

        private void OnRewardedReward(string key)
        {
            if (key == CHEST_REWARD_AD_KEY)
            {
                _availableChestCount.Value += AD_WATCH_REWARD_UNLOCK_CHESTS_COUNT;
                _chestsRewardPopupView.SetTakeRewardButtonActiveState(false);
                _chestsRewardPopupView.SetWatchADButtonActiveState(false);
            }
        }

        private void WatchAD()
        {
            if(_adService.IsRewardedAvailable)
                _adService.ShowRewarded(CHEST_REWARD_AD_KEY);
        }

        private void TakeReward()
        {
            _gameplayDataProvider.GameplayDataProxy.MoneyAmount.Value += _reward;
            _gameplayDataProvider.SaveGameplayData();
            _reward = 0;
            _chestsRewardPopupView.Hide();
            
            _gameStateMachine.EnterIn<ReloadGameState>();
        }

        private void OnChestOpened(int reward)
        {
            if(_availableChestCount.CurrentValue == 0)
                throw new Exception();
                
            _reward += reward;
            _availableChestCount.Value--;
            _openChestsCount++;
            
            if (_availableChestCount.CurrentValue == 0)
            {
                _chestsRewardPopupView.SetTakeRewardButtonActiveState(true);
                _chestsRewardPopupView.SetWatchADButtonActiveState(true);
            }
            
            if(_openChestsCount == MAX_CHEST_COUNT)
                _chestsRewardPopupView.SetWatchADButtonActiveState(false);
        }

        private void HandleGameState(IGameState gameState)
        {
            if (gameState is WinState && _levelRewardService.Reward == LevelReward.Chests)
            {
                _chestsRewardPopupView.Show();
            }
        }

        private int GetRandomReward()
        {
            return Random.Range(1, 5) * 50;
        }
    }
}