using _Project.Utils;
using R3;
using UnityEngine;

namespace _Project.Gameplay
{
    public class Movement
    {
        private const float PLATFORM_WIDTH = 12;
        private const float PLATFORM_BORDER_WIDTH = 1;
        
        private readonly InputHandler _inputHandler;
        private readonly PlayerConfig _config;
        private readonly Transform _playerTransform;
        private readonly ReactiveProperty<bool> _canMove = new ReactiveProperty<bool>();

        private bool _canSlide;

        public ReadOnlyReactiveProperty<bool> OnMove => _canMove;
        
        public Movement(InputHandler inputHandler, PlayerConfig config, Transform playerTransform)
        {
            _inputHandler = inputHandler;
            _config = config;
            _playerTransform = playerTransform;
        }

        public void Enable()
        {
            _canMove.Value = true;
            _canSlide = true;
        }
        
        public void DisableSlide()
        {
            _canSlide = false;
        }
        
        public void Update()
        {
            if (_inputHandler.IsPressing && _canSlide)
                Slide();
            
            if(_canMove.CurrentValue)
                MoveForward();
        }

        private void Slide()
        {
            Vector2 moveDirectionNormalized = _inputHandler.MoveDirection.normalized;
    
            float targetX = _playerTransform.position.x + moveDirectionNormalized.x;
    
            float maxX = PLATFORM_WIDTH / 2 - PLATFORM_BORDER_WIDTH;
            targetX = Mathf.Clamp(targetX, -maxX, maxX);
    
            _playerTransform.position = _playerTransform.position.With(
                x: Mathf.Lerp(_playerTransform.position.x, targetX, _config.SlideMoveSpeed * Time.deltaTime)
            );
        }

        private void MoveForward()
        {
            _playerTransform.position += Vector3.forward * (_config.ForwardMoveSpeed * Time.deltaTime);
        }
    }
}