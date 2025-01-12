using R3;
using UnityEngine;

namespace _Project.Gameplay
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _playerConfig;

        private FinishChunk _finishChunk;
        private InputHandler _inputHandler;
        private Movement _movement;
        private bool _isInit;
        private float _levelDistance;
        private bool _isFinished;
        
        private readonly ReactiveProperty<float> _levelProgress = new ReactiveProperty<float>();

        public ReadOnlyReactiveProperty<float> LevelProgress => _levelProgress;

        public void Init(Level level)
        {
            _finishChunk = level.FinishChunk;
            _inputHandler = new InputHandler();
            _movement = new Movement(_inputHandler, _playerConfig, transform);
            
            _levelDistance = Vector3.Distance(transform.position, _finishChunk.FinishPoint.position);

            _levelProgress.Value = 0;
            _isInit = true;

            _finishChunk.OnFinished += OnFinished;
        }

        private void OnFinished()
        {
            _movement.DisableSlide();
            _levelProgress.Value = 100;
            _isFinished = true;
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
                UpdateLevelDistance();
            }
        }

        private void UpdateLevelDistance()
        {
            if (_isFinished)
                return;
            
            float currentDistance = Vector3.Distance(transform.position, _finishChunk.FinishPoint.position);
            _levelProgress.Value = 100 - ((currentDistance * 100) / _levelDistance);
        }

        private void OnDisable()
        {
            _finishChunk.OnFinished -= OnFinished;
        }
    }
}