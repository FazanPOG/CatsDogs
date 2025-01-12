using UnityEngine;

namespace _Project.Gameplay
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private PlayerCollisionHandler _collisionHandler;

        private InputHandler _inputHandler;
        private Movement _movement;
        private bool _isInit;
        
        public void Init()
        {
            _inputHandler = new InputHandler();
            _movement = new Movement(_inputHandler, _playerConfig, transform);

            _isInit = true;
            
            _collisionHandler.OnFinished += OnFinished;
        }

        private void OnFinished()
        {
            _movement.DisableSlide();
        }

        public void Enable()
        {
            _movement.Enable();
        }
        
        private void Update()
        {
            if (_isInit)
            {
                _movement.Update();
            }
        }

        private void OnDisable()
        {
            _collisionHandler.OnFinished -= OnFinished;
        }
    }
}