using System;
using _Project.Audio;
using UnityEngine;

namespace _Project.Gameplay
{
    [RequireComponent(typeof(BoxCollider))]
    public class FinishChunk : BaseChunk
    {
        [SerializeField] private Transform _finishPoint;
        [SerializeField] private ParticleSystem[] _VFXs;

        private AudioPlayer _audioPlayer;
        
        public Transform FinishPoint => _finishPoint;
        
        public event Action OnFinished;
        
        private void Awake() => GetComponent<BoxCollider>().isTrigger = true;

        public void Init(AudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                foreach (var vfx in _VFXs)
                {
                    vfx.Play();
                    _audioPlayer.PlayFireworkAudio();
                }

                OnFinished?.Invoke();
            }
        }
    }
}