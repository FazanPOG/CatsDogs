using System;
using System.Linq;
using R3;
using UnityEngine;

namespace _Project.Gameplay
{
    public class PlayerView : MonoBehaviour
    {
        private AnimalView[] _animalViews;
        private SkinConfig[] _skins;
        private GameObject _currentSkinView;
        private string _currentSkinID;
        private AnimalView _currentAnimalView;
        private ReadOnlyReactiveProperty<bool> _onMove;
        
        public void Init(
            AnimalView[] animalViews,
            PlayerAnimalMorph animalMorph, 
            SkinConfig[] skins, 
            ISkinService skinService,
            ReadOnlyReactiveProperty<bool> onMove)
        {
            _animalViews = animalViews;
            _skins = skins;
            _onMove = onMove;

            animalMorph.CurrentAnimal.Subscribe(ChangeAnimal);
            skinService.CurrentSkinID.Subscribe(HandleSkinChanged);
            _onMove.Subscribe(HandleOnMove);
        }

        private void ChangeAnimal(Animal animal)
        {
            if(_currentAnimalView != null)
                Destroy(_currentAnimalView.gameObject);
            
            var animalView = _animalViews.FirstOrDefault(x => x.Animal == animal);
            
            if(animalView == null)
                throw new Exception();
            
            _currentAnimalView = Instantiate(animalView, transform);
            _currentAnimalView.Init();
            HandleOnMove(_onMove.CurrentValue);
            HandleSkinChanged(_currentSkinID);
        }

        private void HandleSkinChanged(string skinID)
        {
            if(string.IsNullOrEmpty(skinID))
                return;
            
            if(_currentSkinView != null)
                Destroy(_currentSkinView.gameObject);

            var skinConfig = _skins.FirstOrDefault(x => x.ID == skinID);
            
            if(skinConfig == null)
                throw new Exception();

            var instance = Instantiate(skinConfig.SkinView);
            instance.transform.SetParent(_currentAnimalView.HatTransform);
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = Quaternion.identity;

            _currentSkinView = instance;
            _currentSkinID = skinID;
        }

        private void HandleOnMove(bool onMove)
        {
            if(_currentAnimalView == null)
                throw new Exception();
            
            if (onMove)
            {
                _currentAnimalView.SetRunAnimation();
            }
            else
            {
                _currentAnimalView.SetIdleAnimation();
            }
        }
    }
}