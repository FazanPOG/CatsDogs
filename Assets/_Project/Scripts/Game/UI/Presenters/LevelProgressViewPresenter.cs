using R3;

namespace _Project.UI
{
    public class LevelProgressViewPresenter
    {
        private readonly LevelProgressView _levelProgressView;

        public LevelProgressViewPresenter(
            LevelProgressView levelProgressView, 
            ReadOnlyReactiveProperty<int> levelNumber,
            ReadOnlyReactiveProperty<float> levelProgress)
        {
            _levelProgressView = levelProgressView;

            levelProgress.Subscribe(UpdateProgressBar);
            UpdateText(levelNumber.CurrentValue);
        }
        
        private void UpdateProgressBar(float levelProgress)
        {
            _levelProgressView.SetProgressBarFillAmount(levelProgress / 100);
        }
        
        private void UpdateText(int levelNumber)
        {
            //TODO: localize
            _levelProgressView.SetCurrentLevelText($"Level {levelNumber}");
        }
    }
}