using System;
using System.Linq;
using UnityEngine;

namespace _Project.Gameplay
{
    [RequireComponent(typeof(Animator))]
    public class AnimalView : MonoBehaviour
    {
        private const string RUNNING_ANIMATION_KEY = "isRunning";

        [SerializeField] private Animal _animal;
        [SerializeField] private Transform _hatTransform;

        private Animator _animator;
        private string _idleAnimationKey;
        
        public Animal Animal => _animal;

        public Transform HatTransform => _hatTransform;

        private void Awake() => _animator = GetComponent<Animator>();

        public void Init()
        {
            _idleAnimationKey = GetIdleAnimationKey();
        }
        
        public void SetIdleAnimation()
        {
            DisableAnimations();
            _animator.SetBool(_idleAnimationKey, true);
        }
        
        public void SetRunAnimation()
        {
            DisableAnimations();
            _animator.SetBool(RUNNING_ANIMATION_KEY, true);
        }

        private void DisableAnimations()
        {
            _animator.SetBool(RUNNING_ANIMATION_KEY, false);
            _animator.SetBool(_idleAnimationKey, false);
        }

        private string GetIdleAnimationKey()
        {
            var boolParameters = _animator.parameters.Where(x => x.type == AnimatorControllerParameterType.Bool);
            var firstEnabledBoolParameter = boolParameters.FirstOrDefault(x => x.defaultBool == true)?.name;
            
            if (string.IsNullOrEmpty(firstEnabledBoolParameter))
                throw new Exception("No bool parameter with a default value of true was found.");

            return firstEnabledBoolParameter;
        }
    }
}