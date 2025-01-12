using UnityEngine;

namespace _Project.UI
{
    [CreateAssetMenu(menuName = "_Project/UI/SkinConfig")]
    public class SkinConfig : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private Sprite _itemSprite;

        public string ID => _id;
        public Sprite ItemSprite => _itemSprite;
    }
}