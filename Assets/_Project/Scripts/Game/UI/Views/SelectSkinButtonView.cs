using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.UI
{
    public class SelectSkinButtonView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _unlockStateObject;
        [SerializeField] private GameObject _lockStateObject;
        [SerializeField] private Image _frameImage;
        [SerializeField] private Image _skinImage;

        public event Action OnButtonClicked;

        private void OnEnable()
        {
            _button.onClick.AddListener(() => OnButtonClicked?.Invoke());
        }

        public void SetLockState()
        {
            _lockStateObject.gameObject.SetActive(true);
            _unlockStateObject.gameObject.SetActive(false);
        }

        public void SetUnlockState()
        {
            _lockStateObject.gameObject.SetActive(false);
            _unlockStateObject.gameObject.SetActive(true);
        }

        public void SetButtonInteractable(bool canInteract) => _button.interactable = canInteract;

        public void SetActiveFrameImage(bool activeState) => _frameImage.gameObject.SetActive(activeState);
        public void SetSkinSprite(Sprite sprite) => _skinImage.sprite = sprite;
        
        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}