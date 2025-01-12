using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.UI
{
    public class CurrencyView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _currencyImage;
        [SerializeField] private Color _addCurrencyColor;
        [SerializeField] private Color _spendCurrencyColor;

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
        
        public void SetText(string text) => _text.text = text;

        public void SetCurrencySprite(Sprite sprite) => _currencyImage.sprite = sprite;
        
        public void SetAddCurrencyColor() => _text.color = _addCurrencyColor;
        public void SetSpendCurrencyColor() => _text.color = _spendCurrencyColor;
        public void SetDefaultCurrencyColor() => _text.color = Color.white;
    }
}