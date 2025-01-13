using System;
using _Project.Gameplay;
using ModestTree;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Data
{
    [CreateAssetMenu(menuName = "_Project/Data/DefaultDataConfig")]
    public class DefaultDataConfig : ScriptableObject
    {
        [SerializeField, MinValue(0)] private int _defaultMoney = 0;
        [SerializeField, MinValue(1)] private int _defaultLevelNumber = 1;
        [SerializeField] private SkinConfig[] _defaultUnlockedSkins;
        
        public int DefaultMoney => _defaultMoney;

        public int DefaultLevelNumber => _defaultLevelNumber;

        public SkinConfig[] DefaultUnlockedSkins => _defaultUnlockedSkins;

        private void OnValidate()
        {
            if(_defaultUnlockedSkins.IsEmpty())
                throw new Exception("Must be unlocked minimum 1 skin");
        }
    }
}