using UnityEngine;

namespace _Project.Gameplay
{
    [CreateAssetMenu(menuName = "_Project/Gameplay/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField, Range(0.01f, 50f)] private float _forwardMoveSpeed = 1f;
        [SerializeField, Range(0.01f, 50f)] private float _slideMoveSpeed = 1f;

        public float ForwardMoveSpeed => _forwardMoveSpeed;

        public float SlideMoveSpeed => _slideMoveSpeed;
    }
}