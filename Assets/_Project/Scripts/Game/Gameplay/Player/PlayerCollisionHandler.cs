using System;
using UnityEngine;

namespace _Project.Gameplay
{
    [RequireComponent(typeof(BoxCollider))]
    public class PlayerCollisionHandler : MonoBehaviour
    {
        public event Action OnFinished;
        
        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out FinishChunk finishChunk))
                OnFinished?.Invoke();
        }
    }
}