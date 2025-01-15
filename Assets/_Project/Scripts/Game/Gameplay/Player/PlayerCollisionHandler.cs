using System;
using UnityEngine;

namespace _Project.Gameplay
{
    [RequireComponent(typeof(BoxCollider))]
    public class PlayerCollisionHandler : MonoBehaviour
    {
        public event Action<float> OnItemPicked;
        public event Action OnFinished;
        public event Action OnLevelEnded;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out LevelPickableItem item))
            {
                OnItemPicked?.Invoke(item.MorphValueChanged);
                
                if(item.LevelItem == LevelItem.Pickable)
                    item.DestroySelf();
            }

            if(other.TryGetComponent(out FinishChunk finishChunk))
                OnFinished?.Invoke();
            
            if(other.TryGetComponent(out EndLevelTrigger endLevelTrigger))
                OnLevelEnded?.Invoke();
        }
    }
}