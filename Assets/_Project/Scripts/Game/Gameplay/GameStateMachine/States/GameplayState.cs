namespace _Project.Gameplay
{
    public class GameplayState : IGameState
    {
        private readonly Player _player;
        private readonly Level _level;
        private readonly IGameStateMachine _gameStateMachine;

        public GameplayState(Player player, Level level, IGameStateMachine gameStateMachine)
        {
            _player = player;
            _level = level;
            _gameStateMachine = gameStateMachine;
        }
        
        public void Enter()
        {
            _player.Enable();
            _level.FinishChunk.OnFinished += OnFinished;
        }

        private void OnFinished()
        {
            _gameStateMachine.EnterIn<EndGameAnimationState>();
        }

        public void Exit()
        {
            _level.FinishChunk.OnFinished -= OnFinished;
        }
    }
}