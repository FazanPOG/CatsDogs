using UnityEngine;

namespace _Project.Gameplay
{
    [RequireComponent(typeof(BoxCollider))]
    public class LevelPickableItem : MonoBehaviour
    {
        [SerializeField] private BoxCollider _boxCollider;
        [SerializeField, Range(-1f, 1f)] private float _morphValueChanged;
        [SerializeField] private LevelItem _levelItem;

        public float MorphValueChanged => _morphValueChanged;

        public LevelItem LevelItem => _levelItem;
        
        private void Awake() => _boxCollider.isTrigger = true;

        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}