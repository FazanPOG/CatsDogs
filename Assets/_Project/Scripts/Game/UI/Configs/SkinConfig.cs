using UnityEngine;

namespace _Project.Gameplay
{
    [CreateAssetMenu(menuName = "_Project/Gameplay/SkinConfig")]
    public class SkinConfig : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private Sprite _itemSprite;
        [SerializeField] private GameObject _skinView;

        public string ID => _id;
        public Sprite ItemSprite => _itemSprite;

        public GameObject SkinView => _skinView;
    }
}