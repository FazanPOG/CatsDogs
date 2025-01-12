using System.Collections;
using _Project.Utility;
using UnityEngine;

namespace _Project.Gameplay
{
    public class EndGameAnimationState : IGameState
    {
        private readonly MonoBehaviourContext _monoBehaviourContext;
        private readonly IGameStateMachine _gameStateMachine;

        public EndGameAnimationState(MonoBehaviourContext monoBehaviourContext, IGameStateMachine gameStateMachine)
        {
            _monoBehaviourContext = monoBehaviourContext;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _monoBehaviourContext.StartCoroutine(EndGameAnimation());
        }

        private IEnumerator EndGameAnimation()
        {
            yield return new WaitForSeconds(3f);
            _gameStateMachine.EnterIn<WinState>();
        }
        
        public void Exit()
        {
            
        }
    }
}