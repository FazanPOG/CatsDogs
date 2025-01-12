using _Project.Data;
using R3;

namespace _Project.Gameplay
{
    public class SkinService : ISkinService
    {
        private readonly IGameplayDataProvider _gameplayDataProvider;
        private readonly ReactiveProperty<string> _currentSkinID = new ReactiveProperty<string>();

        public ReadOnlyReactiveProperty<string> CurrentSkinID => _currentSkinID;

        public SkinService(IGameplayDataProvider gameplayDataProvider)
        {
            _gameplayDataProvider = gameplayDataProvider;

            _currentSkinID.Value = _gameplayDataProvider.GameplayDataProxy.CurrentSkinID.CurrentValue;
        }
        
        public void SelectSkin(string id)
        {
            _currentSkinID.Value = id;
            _gameplayDataProvider.GameplayDataProxy.CurrentSkinID.Value = id;
            _gameplayDataProvider.SaveGameplayData();
        }
    }
}