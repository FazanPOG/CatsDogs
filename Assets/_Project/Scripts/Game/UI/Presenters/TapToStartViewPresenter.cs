using _Project.Gameplay;

namespace _Project.UI
{
    public class TapToStartViewPresenter
    {
        private readonly ButtonTextView _tapToStartView;
        private readonly IGameStateMachine _gameStateMachine;

        public TapToStartViewPresenter(ButtonTextView tapToStartView, IGameStateMachine gameStateMachine)
        {
            _tapToStartView = tapToStartView;
            _gameStateMachine = gameStateMachine;
         
            _tapToStartView.Show();
            _tapToStartView.OnButtonClicked += OnButtonClicked;
        }

        private void OnButtonClicked()
        {
            _gameStateMachine.EnterIn<GameplayState>();
            _tapToStartView.Hide();
        }
    }
}