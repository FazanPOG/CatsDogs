using System;
using System.Collections.Generic;
using _Project.API;
using _Project.Data;
using _Project.Game;
using _Project.Utility;
using R3;

namespace _Project.Gameplay
{
    public class GameStateMachine : IGameStateMachine, IGameStateProvider
    {
        private readonly Dictionary<Type, IGameState> _gameStates;

        private readonly ReactiveProperty<IGameState> _currentState = new ReactiveProperty<IGameState>();

        public ReadOnlyReactiveProperty<IGameState> GameState => _currentState;

        public GameStateMachine(
            Player player, 
            Level level,
            IGameplayDataProvider gameplayDataProvider,
            ISceneLoaderService sceneLoaderService,
            MonoBehaviourContext monoBehaviourContext,
            ILevelRewardService levelRewardService,
            IAPIEnvironmentService apiEnvironmentService,
            IADService adService)
        {
            _gameStates = new Dictionary<Type, IGameState>()
            {
                [typeof(BootState)] = new BootState(levelRewardService, apiEnvironmentService),
                [typeof(GameplayState)] = new GameplayState(player, level, this),
                [typeof(EndGameAnimationState)] = new EndGameAnimationState(monoBehaviourContext, this),
                [typeof(WinState)] = new WinState(gameplayDataProvider, monoBehaviourContext),
                [typeof(ReloadGameState)] = new ReloadGameState(sceneLoaderService, adService),
            };
        }

        public void EnterIn<T>() where T : IGameState
        {
            if(_gameStates.TryGetValue(typeof(T), out IGameState gameState) == false)
                throw new Exception();
            
            _currentState.Value?.Exit();
            _currentState.Value = gameState;
            _currentState.Value.Enter();
        }
    }
}