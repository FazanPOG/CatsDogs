using UnityEngine;

namespace _Project.Audio
{
    public class AudioPlayer
    {
        private readonly AudioSource _audioSource;
        private readonly AudioReferencesConfig _audioReferencesConfig;

        public AudioPlayer(AudioSource audioSource, AudioReferencesConfig audioReferencesConfig)
        {
            _audioSource = audioSource;
            _audioReferencesConfig = audioReferencesConfig;
        }

        public bool IsMuted => _audioSource.mute;
        
        public void Mute() => _audioSource.mute = true;
        public void UnMute() => _audioSource.mute = false;
        
        public void PlayBackgroundMusic()
        {
            _audioSource.clip = _audioReferencesConfig.BackgroundMusic;
            _audioSource.loop = true;
            _audioSource.Play();
        }

        public void PlayClickAudio(float volumeScale = 1f) => PlayOneShot(_audioReferencesConfig.ClickClip, volumeScale);
        public void PlayPlopAudio(float volumeScale = 1f) => PlayOneShot(_audioReferencesConfig.PlopClip, volumeScale);
        public void PlayFireworkAudio(float volumeScale = 1f) => PlayOneShot(_audioReferencesConfig.FireworksClip, volumeScale);
        public void PlayCatAudio(float volumeScale = 1f) => PlayOneShot(_audioReferencesConfig.CatClip, volumeScale);
        public void PlayDogAudio(float volumeScale = 1f) => PlayOneShot(_audioReferencesConfig.DogClip, volumeScale);
        
        private void PlayOneShot(AudioClip audioClip, float volumeScale = 1f)
        {
            _audioSource.PlayOneShot(audioClip, volumeScale);
        }
    }
}