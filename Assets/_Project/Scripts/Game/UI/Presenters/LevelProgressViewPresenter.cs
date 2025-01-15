using _Project.API;
using _Project.Gameplay;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.UI
{
    public class LevelProgressViewPresenter : ITickable
    {
        private readonly LevelProgressView _levelProgressView;
        private readonly FinishChunk _finishChunk;
        private readonly Transform _playerTransform;
        private readonly ILocalizationProvider _localizationProvider;
        private readonly float _levelDistance;
        
        private bool _isFinished;
        
        public LevelProgressViewPresenter(
            LevelProgressView levelProgressView, 
            ReadOnlyReactiveProperty<int> levelNumber,
            FinishChunk finishChunk,
            Transform playerTransform,
            IGameStateProvider gameStateProvider,
            ILocalizationProvider localizationProvider)
        {
            _levelProgressView = levelProgressView;
            _finishChunk = finishChunk;
            _playerTransform = playerTransform;
            _localizationProvider = localizationProvider;

            _levelProgressView.SetProgressBarActiveState(_levelProgressView.ShowBar);
            
            if (_levelProgressView.ShowBar)
            {
                _levelDistance = Vector3.Distance(_playerTransform.position, _finishChunk.FinishPoint.position);
                UpdateProgressBar(0f);
            }

            UpdateText(levelNumber.CurrentValue);

            gameStateProvider.GameState.Subscribe(HandleGameState);
        }

        public void Tick()
        {
            if(_isFinished == false && _levelProgressView.ShowBar)
                UpdateLevelDistance();
        }

        private void HandleGameState(IGameState gameState)
        {
            if (gameState is BootState)
                _levelProgressView.Show();
            else
                _levelProgressView.Hide();
        }
        
        private void UpdateLevelDistance()
        {
            float currentDistance = Vector3.Distance(_playerTransform.position, _finishChunk.FinishPoint.position);
            float levelProgress = 100 - ((currentDistance * 100) / _levelDistance);
            UpdateProgressBar(levelProgress);

            if (levelProgress >= 98)
            {
                UpdateProgressBar(100);
                _isFinished = true;
            }
        }

        private void UpdateProgressBar(float levelProgress)
        {
            _levelProgressView.SetProgressBarFillAmount(levelProgress / 100);
        }

        private void UpdateText(int levelNumber)
        {
            string text = $"{_localizationProvider.LocalizationAsset.GetTranslation(LocalizationKeys.LEVEL_KEY)} {levelNumber}";
            _levelProgressView.SetCurrentLevelText(text);
        }
    }
}