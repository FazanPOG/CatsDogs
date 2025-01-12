using System;
using UnityEngine;

namespace _Project.Gameplay
{
    [RequireComponent(typeof(BoxCollider))]
    public class FinishChunk : BaseChunk
    {
        [SerializeField] private Transform _finishPoint;
        [SerializeField] private ParticleSystem[] _VFXs;

        public Transform FinishPoint => _finishPoint;
        
        public event Action OnFinished;
        
        private void Awake() => GetComponent<BoxCollider>().isTrigger = true;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                foreach (var vfx in _VFXs)
                    vfx.Play();
                
                OnFinished?.Invoke();
            }
        }
    }
}