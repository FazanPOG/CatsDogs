using UnityEngine;

namespace _Project.Gameplay
{
    [RequireComponent(typeof(Animator))]
    public class AnimalView : MonoBehaviour
    {
        private const string RUNNING_ANIMATION_KEY = "isRunning";
        private const string IDLE_ANIMATION_KEY = "isIdling";
        
        [SerializeField] private Transform _hatTransform;

        private Animator _animator;

        public Transform HatTransform => _hatTransform;
        
        private void Awake() => _animator = GetComponent<Animator>();

        public void SetIdleAnimation()
        {
            DisableAnimations();
            _animator.SetBool(IDLE_ANIMATION_KEY, true);
        }
        
        public void SetRunAnimation()
        {
            DisableAnimations();
            _animator.SetBool(RUNNING_ANIMATION_KEY, true);
        }

        private void DisableAnimations()
        {
            _animator.SetBool(RUNNING_ANIMATION_KEY, false);
            _animator.SetBool(IDLE_ANIMATION_KEY, false);
        }
    }
}