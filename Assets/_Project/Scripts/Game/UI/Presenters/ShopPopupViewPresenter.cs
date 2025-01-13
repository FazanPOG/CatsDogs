using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Project.API;
using _Project.Data;
using _Project.Gameplay;
using _Project.Utility;
using ModestTree;
using R3;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace _Project.UI
{
    public class ShopPopupViewPresenter
    {
        private const string CURRENCY_AD_REWARD_KEY = nameof(CURRENCY_AD_REWARD_KEY);

        private readonly ButtonTextView _shopButton;
        private readonly ShopPopupView _view;
        private readonly SkinShopConfig _shopConfig;
        private readonly SkinConfig[] _skinConfigs;
        private readonly SelectSkinButtonView _selectSkinButtonViewPrefab;
        private readonly IGameplayDataProvider _gameplayDataProvider;
        private readonly IGameStateProvider _gameStateProvider;
        private readonly IADService _adService;
        private readonly ISkinService _skinService;
        private readonly MonoBehaviourContext _monoBehaviourContext;

        private readonly ReactiveProperty<bool> _canSelectSkin = new ReactiveProperty<bool>();
        private readonly Dictionary<string, SelectSkinButtonViewPresenter> _idSkinButtonPresenterMap = new Dictionary<string, SelectSkinButtonViewPresenter>();
        
        public ShopPopupViewPresenter(
            ButtonTextView shopButton, 
            ShopPopupView view, 
            SkinShopConfig shopConfig,
            SkinConfig[] skinConfigs,
            SelectSkinButtonView selectSkinButtonViewPrefab,
            IGameplayDataProvider gameplayDataProvider,
            IGameStateProvider gameStateProvider,
            IADService adService,
            ISkinService skinService,
            MonoBehaviourContext monoBehaviourContext)
        {
            _shopButton = shopButton;
            _view = view;
            _shopConfig = shopConfig;
            _skinConfigs = skinConfigs;
            _selectSkinButtonViewPrefab = selectSkinButtonViewPrefab;
            _gameplayDataProvider = gameplayDataProvider;
            _gameStateProvider = gameStateProvider;
            _adService = adService;
            _skinService = skinService;
            _monoBehaviourContext = monoBehaviourContext;

            Init();
        }

        private void Init()
        {
            _view.SetUnlockPriceText(_shopConfig.UnlockSkinPrice.ToString());
            _view.SetADRewardText(_shopConfig.ADRewardAmount.ToString());
            
            foreach (var skinConfig in _skinConfigs)
            {
                var skinButtonView = Object.Instantiate(_selectSkinButtonViewPrefab, _view.SkinButtonParentTransform);
                var presenter = new SelectSkinButtonViewPresenter(skinButtonView, skinConfig, _gameplayDataProvider, _skinService, _canSelectSkin);
                _idSkinButtonPresenterMap.Add(skinConfig.ID, presenter);
            }
            
            _shopButton.OnButtonClicked += OpenShop;
            _view.OnCloseButtonClicked += CloseShop;
            _view.OnADButtonClicked += ShowAD;
            _view.OnBuyButtonClicked += OnBuyButtonClicked;
            _adService.OnRewardedReward += TakeADReward;
            _gameStateProvider.GameState.Subscribe(HandleGameState);
            
            _canSelectSkin.Value = true;
        }

        private void HandleGameState(IGameState gameState)
        {
            if (gameState is BootState)
                _shopButton.Show();
            else
                _shopButton.Hide();
        }
        
        private void OnBuyButtonClicked()
        {
            if(CanBuy())
                Buy();
        }

        private void Buy()
        {
            _gameplayDataProvider.GameplayDataProxy.MoneyAmount.Value -= _shopConfig.UnlockSkinPrice;
            
            var lockedSkinIDs = GetLockedSkinIDs();
            int randomIndex = Random.Range(0, lockedSkinIDs.Count);
            string randomSkinID = lockedSkinIDs[randomIndex];
            
            _monoBehaviourContext.StartCoroutine(RandomUnlockAnimation(
                _shopConfig.RandomSkinUnlockAnimationDuration, 
                _shopConfig.RandomSkinUnlockAnimationFlashDuration,
                randomSkinID,
                UnlockSkin));

            _canSelectSkin.Value = false;
        }

        private void UnlockSkin(string id)
        {
            _gameplayDataProvider.GameplayDataProxy.UnlockedSkinIDs.Add(id);
            _skinService.SelectSkin(id);
            _idSkinButtonPresenterMap[id].SetUnlockView();
            _gameplayDataProvider.SaveGameplayData();
            _canSelectSkin.Value = true;
        }
        
        private bool CanBuy()
        {
            bool hasLockedSkins = GetLockedSkinIDs().IsEmpty() == false;
            bool hasEnoughMoney = _gameplayDataProvider.GameplayDataProxy.MoneyAmount.CurrentValue >= _shopConfig.UnlockSkinPrice;
            
            return hasEnoughMoney && hasLockedSkins;
        }
        
        private void TakeADReward(string key)
        {
            if (key == CURRENCY_AD_REWARD_KEY)
            {
                _gameplayDataProvider.GameplayDataProxy.MoneyAmount.Value += _shopConfig.ADRewardAmount;
                _gameplayDataProvider.SaveGameplayData();
            }
        }

        private void ShowAD()
        {
            if(_adService.IsRewardedAvailable)
                _adService.ShowRewarded(CURRENCY_AD_REWARD_KEY);
        }

        private void CloseShop()
        {
            _view.Hide();
        }

        private void OpenShop()
        {
            _view.Show();
        }

        private IEnumerator RandomUnlockAnimation(
            float duration, 
            float flashDuration, 
            string unlockedSkinID,
            Action<string> unlockSkinCallback)
        {
            if (_idSkinButtonPresenterMap.IsEmpty())
                throw new Exception();

            var lockedSkinPresenters = GetLockedSkinPresenters();
            
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                var randomPresenter = lockedSkinPresenters[Random.Range(0, lockedSkinPresenters.Count)];

                randomPresenter.SetActiveFrameImage(true);

                yield return new WaitForSeconds(flashDuration);

                randomPresenter.SetActiveFrameImage(false);

                elapsedTime += flashDuration;
            }

            foreach (var kvp in _idSkinButtonPresenterMap)
                kvp.Value.SetActiveFrameImage(kvp.Key == unlockedSkinID);
            
            unlockSkinCallback?.Invoke(unlockedSkinID);
        }

        private List<string> GetLockedSkinIDs()
        {
            List<string> lockedSkinIDs = new List<string>();

            List<string> unlockedSkinIDs = _gameplayDataProvider.GameplayDataProxy.UnlockedSkinIDs.ToList();

            foreach (var kvp in _idSkinButtonPresenterMap)
            {
                if(unlockedSkinIDs.Contains(kvp.Key) == false)
                    lockedSkinIDs.Add(kvp.Key);
            }

            return lockedSkinIDs;
        }

        private List<SelectSkinButtonViewPresenter> GetLockedSkinPresenters()
        {
            List<SelectSkinButtonViewPresenter> lockedSkinPresenters = new List<SelectSkinButtonViewPresenter>();

            List<string> unlockedSkinIDs = _gameplayDataProvider.GameplayDataProxy.UnlockedSkinIDs.ToList();

            foreach (var kvp in _idSkinButtonPresenterMap)
            {
                if(unlockedSkinIDs.Contains(kvp.Key) == false)
                    lockedSkinPresenters.Add(kvp.Value);
            }

            return lockedSkinPresenters;
        }
    }
}