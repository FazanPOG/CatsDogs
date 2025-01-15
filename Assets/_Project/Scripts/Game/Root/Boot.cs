using System.Collections;
using _Project.API;
using _Project.Game;
using _Project.Utility;
using UnityEngine;

namespace _Project.Root
{
    public class Boot
    {
        private readonly ISceneLoaderService _sceneLoaderService;
        private readonly IAPIEnvironmentService _apiEnvironmentService;
        private readonly ILocalizationProvider _localizationProvider;

        public Boot(
            MonoBehaviourContext context, 
            ISceneLoaderService sceneLoaderService, 
            IAPIEnvironmentService apiEnvironmentService,
            ILocalizationProvider localizationProvider)
        {
            _sceneLoaderService = sceneLoaderService;
            _apiEnvironmentService = apiEnvironmentService;
            _localizationProvider = localizationProvider;

            context.StartCoroutine(WaitAPILoad());
        }

        private IEnumerator WaitAPILoad()
        {
            yield return new WaitUntil(() => _apiEnvironmentService.IsReady);
            _localizationProvider.LoadLocalizationAsset();
            StartGame();
        }

        private void StartGame()
        {
            _sceneLoaderService.LoadGameplayScene();
        }
    }
}