namespace _Project.Data
{
    public interface IGameplayDataProvider
    {
        GameplayDataProxy GameplayDataProxy { get; }
        GameplayDataProxy LoadGameplayData();
        void SaveGameplayData();
    }
}