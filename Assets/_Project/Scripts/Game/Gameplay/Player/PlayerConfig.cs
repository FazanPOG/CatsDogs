using UnityEngine;

namespace _Project.Gameplay
{
    [CreateAssetMenu(menuName = "_Project/Gameplay/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [Header("Settings")]
        [SerializeField, Range(0.01f, 50f)] private float _forwardMoveSpeed = 1f;
        [SerializeField, Range(0.01f, 50f)] private float _slideMoveSpeed = 1f;
        [SerializeField, Range(0f, 1f)] private float _initialMorphValue = 0.5f;
        [Header("View")] 
        [SerializeField] private AnimalView[] _animalViewPrefabs;

        public float ForwardMoveSpeed => _forwardMoveSpeed;

        public float SlideMoveSpeed => _slideMoveSpeed;

        public float InitialMorphValue => _initialMorphValue;
        
        public AnimalView[] AnimalViewPrefabs => _animalViewPrefabs;
    }
}