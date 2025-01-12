using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.UI
{
    public class ShopPopupView : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _adButton;
        [SerializeField] private TextMeshProUGUI _unlockPriceText;
        [SerializeField] private TextMeshProUGUI _adRewardText;
        [SerializeField] private Image[] _currencyImages;
        [SerializeField] private RectTransform _skinButtonParentTransform;

        public event Action OnCloseButtonClicked;
        public event Action OnBuyButtonClicked;
        public event Action OnADButtonClicked;

        public RectTransform SkinButtonParentTransform => _skinButtonParentTransform;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(() => OnCloseButtonClicked?.Invoke());
            _buyButton.onClick.AddListener(() => OnBuyButtonClicked?.Invoke());
            _adButton.onClick.AddListener(() => OnADButtonClicked?.Invoke());
        }

        private void Start()
        {
            Hide();
        }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        public void SetUnlockPriceText(string text) => _unlockPriceText.text = text;
        public void SetADRewardText(string text) => _adRewardText.text = text; 
        
        public void SetCurrencySprite(Sprite sprite)
        {
            foreach (var image in _currencyImages)
                image.sprite = sprite;
        } 
        
        private void OnDisable()
        {
            _closeButton.onClick.RemoveAllListeners();
            _buyButton.onClick.RemoveAllListeners();
            _adButton.onClick.RemoveAllListeners();
        }
    }
}