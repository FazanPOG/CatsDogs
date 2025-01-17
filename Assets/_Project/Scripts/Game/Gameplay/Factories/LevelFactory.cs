using UnityEngine;
using Zenject;

namespace _Project.Gameplay
{
    public class LevelFactory
    {
        private readonly Vector3 SPAWN_OFFSET = new Vector3(0f, 0f, 20f);
        private readonly DiContainer _container;
        private readonly GameplayConfig _gameplayConfig;
        private readonly Transform _parent;

        public LevelFactory(DiContainer container, GameplayConfig gameplayConfig, Transform parent)
        {
            _container = container;
            _gameplayConfig = gameplayConfig;
            _parent = parent;
        }
        
        public Level Create(int levelNumber)
        {
            int index = levelNumber - 1;
            var prefab = _gameplayConfig.LevelPrefabs[index];
            
            var instance = _container
                .InstantiatePrefabForComponent<Level>(prefab, _parent.transform.position + SPAWN_OFFSET, Quaternion.identity, _parent);

            return instance;
        }
    }
}