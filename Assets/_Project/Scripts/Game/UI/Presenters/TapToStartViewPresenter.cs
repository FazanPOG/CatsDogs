using _Project.API;
using _Project.Audio;
using _Project.Gameplay;

namespace _Project.UI
{
    public class TapToStartViewPresenter
    {
        private readonly ButtonTextView _tapToStartView;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly AudioPlayer _audioPlayer;

        public TapToStartViewPresenter(ButtonTextView tapToStartView, IGameStateMachine gameStateMachine, AudioPlayer audioPlayer, ILocalizationProvider localizationProvider)
        {
            _tapToStartView = tapToStartView;
            _gameStateMachine = gameStateMachine;
            _audioPlayer = audioPlayer;

            _tapToStartView.SetText(localizationProvider.LocalizationAsset.GetTranslation(LocalizationKeys.PRESS_TO_START_KEY));
            _tapToStartView.Show();
            _tapToStartView.OnButtonClicked += OnButtonClicked;
        }

        private void OnButtonClicked()
        {
            _gameStateMachine.EnterIn<GameplayState>();
            _tapToStartView.Hide();
            _audioPlayer.PlayClickAudio();
        }
    }
}