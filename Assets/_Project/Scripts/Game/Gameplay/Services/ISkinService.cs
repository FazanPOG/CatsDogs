using R3;

namespace _Project.Gameplay
{
    public interface ISkinService
    {
        ReadOnlyReactiveProperty<string> CurrentSkinID { get; }
        void SelectSkin(string id);
    }
}