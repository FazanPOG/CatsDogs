using _Project.UI;
using UnityEngine;

namespace _Project.Gameplay
{
    [CreateAssetMenu(menuName = "_Project/Gameplay/GameplayConfig")]
    public class GameplayConfig : ScriptableObject
    {
        [SerializeField] private Level[] _levelPrefabs;
        [SerializeField] private SkinConfig[] _skinConfigs;

        public Level[] LevelPrefabs => _levelPrefabs;

        public SkinConfig[] SkinConfigs => _skinConfigs;
    }
}