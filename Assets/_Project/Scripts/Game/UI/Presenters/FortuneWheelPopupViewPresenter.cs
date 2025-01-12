using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Project.API;
using _Project.Data;
using _Project.Gameplay;
using _Project.Utility;
using DG.Tweening;
using R3;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.UI
{
    public class FortuneWheelPopupViewPresenter
    {
        private const string WHEEL_BONUS_REWARD_KEY = nameof(WHEEL_BONUS_REWARD_KEY);
        private const int REWARD_AD_BONUS = 3;
        private const int NUMBER_OF_REWARDS = 6;
        
        private readonly FortuneWheelPopupView _view;
        private readonly IReadOnlyList<FortuneWheelRewardView> _rewardViews;
        private readonly List<FortuneWheelRewardConfig> _rewardConfigs;
        private readonly SpriteReferencesConfig _spriteReferencesConfig;
        private readonly IGameplayDataProvider _gameplayDataProvider;
        private readonly IGameStateProvider _gameStateProvider;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ILevelRewardService _levelRewardService;
        private readonly IADService _adService;
        private readonly MonoBehaviourContext _monoBehaviourContext;

        private int _moneyReward;
        
        public FortuneWheelPopupViewPresenter(
            FortuneWheelPopupView view, 
            List<FortuneWheelRewardConfig> rewardConfigs,
            SpriteReferencesConfig spriteReferencesConfig,
            IGameplayDataProvider gameplayDataProvider,
            IGameStateProvider gameStateProvider,
            IGameStateMachine gameStateMachine,
            ILevelRewardService levelRewardService,
            IADService adService,
            MonoBehaviourContext monoBehaviourContext)
        {
            _view = view;
            _rewardViews = _view.FortuneWheelRewardViews;
            _rewardConfigs = rewardConfigs;
            _spriteReferencesConfig = spriteReferencesConfig;
            _gameplayDataProvider = gameplayDataProvider;
            _gameStateProvider = gameStateProvider;
            _gameStateMachine = gameStateMachine;
            _levelRewardService = levelRewardService;
            _adService = adService;
            _monoBehaviourContext = monoBehaviourContext;

            Init();
        }

        private void Init()
        {
            for (var i = 0; i < _rewardViews.Count; i++)
                new FortuneWheelRewardViewPresenter(_rewardViews[i], _rewardConfigs[i], _spriteReferencesConfig);

            _view.SetSpinButtonActiveState(true);
            _view.SetTakeRewardButtonActiveState(false);
            _view.SetTakeADBonusedRewardButtonActiveState(false);

            _gameStateProvider.GameState.Subscribe(HandleGameState);
            _view.OnSpinButtonClicked += SpinWheel;
            _view.OnTakeRewardButtonClicked += TakeReward;
            _view.OnTakeADBonusedRewardButtonClicked += ShowAD;
            _adService.OnRewardedReward += OnRewarded;
            
            _view.Hide();
        }

        private void SpinWheel()
        {
            int fullRotationsCount = Random.Range(3, 8);
            var randomConfig = _rewardConfigs[Random.Range(0, _rewardConfigs.Count)];
            _monoBehaviourContext.StartCoroutine(SpinCoroutine(fullRotationsCount, _rewardConfigs.IndexOf(randomConfig), randomConfig.ID, OnSpinStopped));
        }

        private void OnSpinStopped(string id)
        {
            _view.SetSpinButtonActiveState(false);
            _view.SetTakeRewardButtonActiveState(true);
            _view.SetTakeADBonusedRewardButtonActiveState(true);

            var rewardConfig = _rewardConfigs.First(x => x.ID == id);
            _moneyReward = rewardConfig.RewardValue;
        }
        
        private void ShowAD()
        {
            if(_adService.IsRewardedAvailable)
                _adService.ShowRewarded(WHEEL_BONUS_REWARD_KEY);
        }

        private void OnRewarded(string key)
        {
            if (key == WHEEL_BONUS_REWARD_KEY)
            {
                _moneyReward *= REWARD_AD_BONUS;
                TakeReward();
            }
        }

        private void TakeReward()
        {
            if(_moneyReward == 0)
                throw new Exception();
            
            _gameplayDataProvider.GameplayDataProxy.MoneyAmount.Value += _moneyReward;
            _gameplayDataProvider.SaveGameplayData();
            _view.Hide();
            
            _gameStateMachine.EnterIn<ReloadGameState>();
        }
        
        private void HandleGameState(IGameState newGameState)
        {
            if (newGameState is WinState && _levelRewardService.Reward == LevelReward.FortuneWheel)
            {
                _view.SetSpinButtonActiveState(true);
                _view.SetTakeRewardButtonActiveState(false);
                _view.SetTakeADBonusedRewardButtonActiveState(false);
                _view.Show();
            }
        }

        private IEnumerator SpinCoroutine(int fullRotationsCount, int rewardIndex, string rewardID, Action<string> onSpinComplete)
        {
            float anglePerReward = 360 / NUMBER_OF_REWARDS;

            float targetAngle = rewardIndex * anglePerReward;
            
            float finalAngle = 360 * fullRotationsCount + targetAngle;

            bool isComplete = false;
            _view.WheelTransform
                .DORotate(new Vector3(0, 0, finalAngle), 4f, RotateMode.FastBeyond360)
                .SetEase(Ease.OutQuart)
                .OnComplete(() => isComplete = true);

            while (!isComplete)
            {
                yield return null;
            }

            onSpinComplete?.Invoke(rewardID);
        }
    }
}