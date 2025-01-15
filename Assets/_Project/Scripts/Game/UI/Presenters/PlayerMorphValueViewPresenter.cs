using R3;
using UnityEngine;

namespace _Project.UI
{
    public class PlayerMorphValueViewPresenter
    {
        private readonly PlayerMorphValueView _view;

        public PlayerMorphValueViewPresenter(PlayerMorphValueView view, ReadOnlyReactiveProperty<float> playerMorphValue)
        {
            _view = view;
            
            playerMorphValue.Subscribe(UpdateView);
        }

        private void UpdateView(float morphValue)
        {
            var newXPos = MapValue(morphValue, _view.MinPinXValuePosition, _view.MaxPinXValuePosition);
            
            _view.SetPinXPosition(newXPos);
        }
        
        private float MapValue(float value, float min, float max)
        {
            return min + (max - min) * value;
        }
    }
}