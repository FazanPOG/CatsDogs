using _Project.Utility;
using UnityEngine.SceneManagement;

namespace _Project.Game
{
    public class SceneLoaderService : ISceneLoaderService
    {
        public void LoadGameplayScene()
        {
            SceneManager.LoadScene(Scenes.Gameplay);
        }
    }
}