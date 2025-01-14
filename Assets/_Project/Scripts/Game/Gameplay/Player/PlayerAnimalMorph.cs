using System;
using System.Collections.Generic;
using System.Linq;
using R3;
using UnityEngine;

namespace _Project.Gameplay
{
    public class PlayerAnimalMorph
    {
        private readonly ReactiveProperty<float> _currentMorphValue = new ReactiveProperty<float>();
        private readonly ReactiveProperty<Animal> _currentAnimal = new ReactiveProperty<Animal>();
        private readonly Dictionary<Animal, float> _animalValueMap;

        public ReadOnlyReactiveProperty<float> MorphValue => _currentMorphValue;
        public ReadOnlyReactiveProperty<Animal> CurrentAnimal => _currentAnimal;
        
        public PlayerAnimalMorph(float initialValue)
        {
            _currentMorphValue.Value = initialValue;
            
            _animalValueMap = new Dictionary<Animal, float>()
            {
                [Animal.SmallCat] = 0.5f,
                [Animal.BigCat] = 0.1f,
                [Animal.SmallDog] = 0.7f,
                [Animal.BigDog] = 0.9f,
            };
            
            CheckCurrentAnimal();
        }

        public void MoveValueRight(float value)
        {
            if(value < 0)
                throw new Exception();

            Mathf.Clamp01(_currentMorphValue.Value += value);
            
            CheckCurrentAnimal();
        }

        public void MoveValueLeft(float value)
        {
            if(value < 0)
                throw new Exception();

            Mathf.Clamp01(_currentMorphValue.Value -= value);
            
            CheckCurrentAnimal();
        }

        private void CheckCurrentAnimal()
        {
            Animal newAnimal = GetCurrentAnimal();
            
            if (_currentAnimal.Value != newAnimal)
                _currentAnimal.Value = newAnimal;
        }
        
        private Animal GetCurrentAnimal()
        {
            var sortedAnimals = _animalValueMap.OrderBy(kvp => kvp.Value).ToList();

            for (int i = 0; i < sortedAnimals.Count - 1; i++)
            {
                float currentValue = sortedAnimals[i].Value;
                float nextValue = sortedAnimals[i + 1].Value;

                if (_currentMorphValue.Value >= currentValue && _currentMorphValue.Value <= nextValue)
                {
                    return sortedAnimals[i + 1].Key;
                }
            }

            return _currentMorphValue.Value < sortedAnimals.First().Value
                ? sortedAnimals.First().Key
                : sortedAnimals.Last().Key;
        }
    }
}