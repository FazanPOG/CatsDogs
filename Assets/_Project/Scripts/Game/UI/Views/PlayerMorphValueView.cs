using UnityEngine;

namespace _Project.UI
{
    public class PlayerMorphValueView : MonoBehaviour
    {
        private const float MIN_PIN_VALUE_X_POSITION = -130f;
        private const float MAX_PIN_VALUE_X_POSITION = 130f;
        
        [SerializeField] private RectTransform _pinTransform;

        public float MinPinXValuePosition => MIN_PIN_VALUE_X_POSITION;
        public float MaxPinXValuePosition => MAX_PIN_VALUE_X_POSITION;
        
        public void SetPinXPosition(float xPos)
        {
            Vector2 currentPosition = _pinTransform.anchoredPosition;
            currentPosition.x = xPos;
            _pinTransform.anchoredPosition = currentPosition;
        }
    }
}