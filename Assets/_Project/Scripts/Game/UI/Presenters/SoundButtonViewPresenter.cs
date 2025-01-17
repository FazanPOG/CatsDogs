using _Project.Audio;

namespace _Project.UI
{
    public class SoundButtonViewPresenter
    {
        private readonly SoundButtonView _soundButtonView;
        private readonly AudioPlayer _audioPlayer;

        private bool _isMuted;
        
        public SoundButtonViewPresenter(SoundButtonView soundButtonView, AudioPlayer audioPlayer)
        {
            _soundButtonView = soundButtonView;
            _audioPlayer = audioPlayer;

            _isMuted = audioPlayer.IsMuted;
            HandleMute();
            
            _soundButtonView.OnButtonClicked += HandleMute;
        }

        private void HandleMute()
        {
            if (_isMuted)
            {
                _audioPlayer.UnMute();
                _soundButtonView.SetUnMuteIcon();
            }
            else
            {
                _audioPlayer.Mute();
                _soundButtonView.SetMuteIcon();
            }

            _isMuted = !_isMuted;
            _audioPlayer.PlayClickAudio();
        }
    }
}