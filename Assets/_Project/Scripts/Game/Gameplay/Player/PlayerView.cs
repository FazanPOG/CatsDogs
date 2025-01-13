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
        private AnimalView _currentAnimalView;
        
        public void Init(
            AnimalView[] animalViews,
            PlayerAnimalMorph animalMorph, 
            SkinConfig[] skins, 
            ISkinService skinService,
            ReadOnlyReactiveProperty<bool> onMove)
        {
            _animalViews = animalViews;
            _skins = skins;

            animalMorph.CurrentAnimal.Subscribe(ChangeAnimal);
            skinService.CurrentSkinID.Subscribe(HandleSkinChanged);
            onMove.Subscribe(HandleOnMove);
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
        }

        private void HandleSkinChanged(string skinID)
        {
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