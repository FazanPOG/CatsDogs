using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.UI
{
    public class SoundButtonView : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private Sprite _unMuteSprite;
        [SerializeField] private Sprite _muteSprite;
        [SerializeField] private Button _button;

        public event Action OnButtonClicked;
        
        private void OnEnable()
        {
            _button.onClick.AddListener(() => OnButtonClicked?.Invoke());
        }

        public void SetMuteIcon() => _iconImage.sprite = _muteSprite;
        public void SetUnMuteIcon() => _iconImage.sprite = _unMuteSprite;
        
        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}