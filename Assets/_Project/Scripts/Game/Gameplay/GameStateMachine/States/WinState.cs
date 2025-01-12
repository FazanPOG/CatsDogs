using System.Collections;
using _Project.Data;
using _Project.Utility;
using UnityEngine;

namespace _Project.Gameplay
{
    public class WinState : IGameState
    {
        private readonly IGameplayDataProvider _gameplayDataProvider;
        private readonly MonoBehaviourContext _monoBehaviourContext;

        public WinState(IGameplayDataProvider gameplayDataProvider, MonoBehaviourContext monoBehaviourContext)
        {
            _gameplayDataProvider = gameplayDataProvider;
            _monoBehaviourContext = monoBehaviourContext;
        }
        
        public void Enter()
        {
            _gameplayDataProvider.GameplayDataProxy.LevelNumber.Value += 1;
            _gameplayDataProvider.SaveGameplayData();
            _monoBehaviourContext.StartCoroutine(WaitEndGameAnimations());
        }

        //TODO: new state: EndGameAnimationState
        private IEnumerator WaitEndGameAnimations()
        {
            yield return new WaitForSeconds(2f);
        }
        
        public void Exit()
        {
            
        }
    }
}