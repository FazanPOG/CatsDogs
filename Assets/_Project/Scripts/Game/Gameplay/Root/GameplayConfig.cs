using UnityEngine;

namespace _Project.Gameplay
{
    [CreateAssetMenu(menuName = "_Project/Gameplay/GameplayConfig")]
    public class GameplayConfig : ScriptableObject
    {
        [SerializeField] private Level[] _levelPrefabs;

        public Level[] LevelPrefabs => _levelPrefabs;
    }
}