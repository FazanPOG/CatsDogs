using UnityEngine;

namespace _Project.UI
{
    [CreateAssetMenu(menuName = "_Project/UI/SpriteReferencesConfig")]
    public class SpriteReferencesConfig : ScriptableObject
    {
        [SerializeField] private Sprite _currencySprite;

        public Sprite CurrencySprite => _currencySprite;
    }
}