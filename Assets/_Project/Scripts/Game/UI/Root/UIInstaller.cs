using System;
using System.Collections.Generic;
using _Project.API;
using _Project.Audio;
using _Project.Data;
using _Project.Gameplay;
using _Project.UI;
using _Project.Utility;
using UnityEngine;
using Zenject;

namespace _Project.Game
{
    public class UIInstaller : MonoInstaller
    {
        private const int FORTUNE_WHEEL_REWARD_COUNT = 6;
        
        [Header("Configs")]
        [SerializeField] private SpriteReferencesConfig _spriteReferences;
        [SerializeField] private SkinShopConfig _shopConfig;
        [SerializeField] private List<FortuneWheelRewardConfig> _wheelRewardConfigs;
        [Header("Prefabs")] 
        [SerializeField] private SelectSkinButtonView _skinButtonPrefab;
        [Header("HUD")]
        [SerializeField] private CurrencyView[] _currencyViews;
        [SerializeField] private ButtonTextView _tapToStartView;
        [SerializeField] private LevelProgressView _levelProgressView;
        [SerializeField] private ButtonTextView _shopButton;
        [SerializeField] private PlayerMorphValueView _morphValueView;
        [SerializeField] private SoundButtonView _soundButtonView;
        [Header("Popups")]
        [SerializeField] private ShopPopupView _shopPopupView;
        [SerializeField] private FortuneWheelPopupView _fortuneWheelPopupView;
        [SerializeField] private ChestsRewardPopupView _chestsRewardPopupView;

        public override void InstallBindings()
        {
            var gameplayDataProvider = Container.Resolve<IGameplayDataProvider>();
            var gameStateMachine = Container.Resolve<IGameStateMachine>();
            var gameStateProvider = Container.Resolve<IGameStateProvider>();
            var adService = Container.Resolve<IADService>();
            var skinService = Container.Resolve<ISkinService>();
            var context = Container.Resolve<MonoBehaviourContext>();
            var player = Container.Resolve<Player>();
            var level = Container.Resolve<Level>();
            var levelRewardService = Container.Resolve<ILevelRewardService>();
            var localizationProvider = Container.Resolve<ILocalizationProvider>();
            var gameplayConfig = Container.Resolve<GameplayConfig>();
            var audioPlayer = Container.Resolve<AudioPlayer>();

            foreach (var currencyView in _currencyViews)
                new CurrencyViewPresenter(currencyView, gameplayDataProvider.GameplayDataProxy.MoneyAmount, _spriteReferences, gameStateProvider);
            
            new TapToStartViewPresenter(_tapToStartView, gameStateMachine, audioPlayer, localizationProvider);
            
            var levelProgressViewPresenter = new LevelProgressViewPresenter(
                _levelProgressView, 
                gameplayDataProvider.GameplayDataProxy.LevelNumber, 
                level.FinishChunk,
                player.transform,
                gameStateProvider,
                localizationProvider);

            Container.Bind<ITickable>().To<LevelProgressViewPresenter>().FromInstance(levelProgressViewPresenter).AsCached().NonLazy();
            
            new ShopPopupViewPresenter(
                _shopButton, 
                _shopPopupView, 
                _shopConfig, 
                gameplayConfig.SkinConfigs,
                _skinButtonPrefab, 
                gameplayDataProvider, 
                gameStateProvider,
                adService, 
                skinService,
                context,
                localizationProvider,
                audioPlayer);
            
            new FortuneWheelPopupViewPresenter(
                _fortuneWheelPopupView,
                _wheelRewardConfigs,
                _spriteReferences,
                gameplayDataProvider,
                gameStateProvider,
                gameStateMachine,
                levelRewardService, 
                adService,
                context,
                audioPlayer,
                localizationProvider);

            new ChestsRewardPopupViewPresenter(
                _chestsRewardPopupView, 
                _spriteReferences, 
                gameplayDataProvider, 
                gameStateMachine, 
                gameStateProvider,
                levelRewardService,
                adService);

            new PlayerMorphValueViewPresenter(_morphValueView, player.MorphValue);

            new SoundButtonViewPresenter(_soundButtonView, audioPlayer);
        }

        private void OnValidate()
        {
            if (_wheelRewardConfigs.Count != FORTUNE_WHEEL_REWARD_COUNT)
                throw new Exception($"The number of wheel reward configurations ({_wheelRewardConfigs.Count}) does not match the expected count ({FORTUNE_WHEEL_REWARD_COUNT}).");
        }
    }
}