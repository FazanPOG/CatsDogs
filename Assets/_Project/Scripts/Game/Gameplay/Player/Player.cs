using R3;
using UnityEngine;

namespace _Project.Gameplay
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private PlayerCollisionHandler _collisionHandler;
        [SerializeField] private PlayerView _playerView;

        private InputHandler _inputHandler;
        private Movement _movement;
        private PlayerAnimalMorph _animalMorph;
        private bool _isInit;
        
        public ReadOnlyReactiveProperty<float> MorphValue => _animalMorph.MorphValue;
        
        public void Init(SkinConfig[] skinConfigs, ISkinService skinService)
        {
            _inputHandler = new InputHandler();
            _movement = new Movement(_inputHandler, _playerConfig, transform);
            _animalMorph = new PlayerAnimalMorph(_playerConfig.InitialMorphValue);
            _playerView.Init(_playerConfig.AnimalViewPrefabs, _animalMorph, skinConfigs, skinService, _movement.OnMove);
            
            _isInit = true;
            
            _collisionHandler.OnItemPicked += OnItemPicked;
            _collisionHandler.OnFinished += OnFinished;
        }

        private void OnItemPicked(float morphValueChanged)
        {
            if(morphValueChanged < 0)
                _animalMorph.MoveValueLeft(Mathf.Abs(morphValueChanged));
            else
                _animalMorph.MoveValueRight(Mathf.Abs(morphValueChanged));
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
            _collisionHandler.OnItemPicked -= OnItemPicked;
            _collisionHandler.OnFinished -= OnFinished;
        }
    }
}