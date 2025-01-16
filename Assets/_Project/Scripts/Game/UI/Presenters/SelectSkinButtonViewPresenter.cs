using System;
using _Project.Audio;
using _Project.Data;
using _Project.Gameplay;
using R3;

namespace _Project.UI
{
    public class SelectSkinButtonViewPresenter
    {
        private readonly SelectSkinButtonView _selectSkinButtonView;
        private readonly SkinConfig _skinConfig;
        private readonly IGameplayDataProvider _gameplayDataProvider;
        private readonly ISkinService _skinService;
        private readonly AudioPlayer _audioPlayer;

        private bool _isUnlocked;
        
        public SelectSkinButtonViewPresenter(
            SelectSkinButtonView selectSkinButtonView, 
            SkinConfig skinConfig, 
            IGameplayDataProvider gameplayDataProvider,
            ISkinService skinService,
            ReadOnlyReactiveProperty<bool> canSelectSkin,
            AudioPlayer audioPlayer)
        {
            _selectSkinButtonView = selectSkinButtonView;
            _skinConfig = skinConfig;
            _gameplayDataProvider = gameplayDataProvider;
            _skinService = skinService;
            _audioPlayer = audioPlayer;

            InitView();
            _selectSkinButtonView.OnButtonClicked += OnButtonClicked;
            _skinService.CurrentSkinID.Subscribe(OnSkinChanged);
            canSelectSkin.Skip(1).Subscribe(OnCanSelectSkinStateChanged);
        }
        
        private void InitView()
        {
            _isUnlocked = _gameplayDataProvider.GameplayDataProxy.UnlockedSkinIDs.Contains(_skinConfig.ID);

            if (_isUnlocked)
                _selectSkinButtonView.SetUnlockState();
            else
                _selectSkinButtonView.SetLockState();
            
            _selectSkinButtonView.SetSkinSprite(_skinConfig.ItemSprite);
        }

        public void SetActiveFrameImage(bool activeState) => _selectSkinButtonView.SetActiveFrameImage(activeState);

        public void SetUnlockView()
        {
            _isUnlocked = _gameplayDataProvider.GameplayDataProxy.UnlockedSkinIDs.Contains(_skinConfig.ID);
            
            if(_isUnlocked == false)
                throw new Exception($"Try set unlock view, but skin is locked, ID: {_skinConfig.ID}");
                
            _selectSkinButtonView.SetUnlockState();
        }

        private void OnCanSelectSkinStateChanged(bool canSelect)
        {
            _selectSkinButtonView.SetButtonInteractable(canSelect);
            
            if (canSelect == false)
                _selectSkinButtonView.SetActiveFrameImage(false);
        }

        private void OnButtonClicked()
        {
            if(_isUnlocked == false)
                return;
            
            _audioPlayer.PlayClickAudio();
            _skinService.SelectSkin(_skinConfig.ID);
        }

        private void OnSkinChanged(string skinID)
        {
            _selectSkinButtonView.SetActiveFrameImage(skinID == _skinConfig.ID);
        }
    }
}