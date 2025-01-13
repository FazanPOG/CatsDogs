using _Project.Data;
using _Project.Game;
using _Project.Utility;
using UnityEngine;
using Zenject;

namespace _Project.Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private DefaultDataConfig _defaultDataConfig;
        [SerializeField] private GameplayConfig _gameplayConfig;
        [SerializeField] private Transform _levelParentTransform;
        [SerializeField] private Player _player;
        [SerializeField] private CameraSystem _cameraSystem;

        public override void InstallBindings()
        {
            BindData();
            BindConfigs();
            BindFactories();
            BindServices();
            BindLevel();
            BindPlayer();

            BindGameStateMachine();

            Container.Resolve<IGameStateMachine>().EnterIn<BootState>();
        }

        private void BindData()
        {
            PlayerPrefsGameplayDataProvider gameplayDataProvider = new PlayerPrefsGameplayDataProvider(_defaultDataConfig);
            gameplayDataProvider.LoadGameplayData();
            Container
                .Bind<IGameplayDataProvider>()
                .To<PlayerPrefsGameplayDataProvider>()
                .FromInstance(gameplayDataProvider)
                .AsSingle()
                .NonLazy();
        }

        private void BindConfigs()
        {
            Container.Bind<GameplayConfig>().FromInstance(_gameplayConfig).AsSingle().NonLazy();
        }

        private void BindFactories()
        {
            LevelFactory levelFactory = new LevelFactory(Container, _gameplayConfig, _levelParentTransform);
            Container.Bind<LevelFactory>().FromInstance(levelFactory).AsSingle().NonLazy();
        }

        private void BindServices()
        {
            Container.Bind<ISkinService>().To<SkinService>().FromNew().AsSingle().NonLazy();
            Container.Bind<ILevelRewardService>().To<LevelRewardService>().FromNew().AsSingle().NonLazy();
        }

        private void BindLevel()
        {
            var gameplayDataProvider =  Container.Resolve<IGameplayDataProvider>();
            var levelFactory =  Container.Resolve<LevelFactory>();
            
            int currentLevelNumber = gameplayDataProvider.GameplayDataProxy.LevelNumber.CurrentValue;

            Level level;
            if (_gameplayConfig.LevelPrefabs.Length < currentLevelNumber)
                level = levelFactory.Create(getRandomLevelNumber());
            else
                level = levelFactory.Create(currentLevelNumber);

            Container.Bind<Level>().FromInstance(level).AsSingle().NonLazy();
            
            int getRandomLevelNumber()
            {
                int index = Random.Range(0, _gameplayConfig.LevelPrefabs.Length);
                return index + 1;
            }
        }

        private void BindPlayer()
        {
            var skinService = Container.Resolve<ISkinService>();
            
            _player.Init(_gameplayConfig.SkinConfigs, skinService);
            _cameraSystem.Follow(_player.transform);

            Container.Bind<Player>().FromInstance(_player).AsSingle().NonLazy();
        }

        private void BindGameStateMachine()
        {
            var level = Container.Resolve<Level>();
            var gameplayDataProvider = Container.Resolve<IGameplayDataProvider>();
            var sceneLoaderService = Container.Resolve<ISceneLoaderService>();
            var monoBehaviourContext = Container.Resolve<MonoBehaviourContext>();
            var levelRewardService = Container.Resolve<ILevelRewardService>();
            
            GameStateMachine gameStateMachine = new GameStateMachine(
                _player, 
                level, 
                gameplayDataProvider, 
                sceneLoaderService, 
                monoBehaviourContext,
                levelRewardService);

            Container.BindInterfacesTo<GameStateMachine>().FromInstance(gameStateMachine).AsSingle().NonLazy();
        }
    }
}