using _Project.API;
using _Project.Game;
using _Project.Utility;
using UnityEngine;
using Zenject;

namespace _Project.Root
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindUtils();
            BindAPI();
            BindServices();

            StartGame();
        }

        private void BindUtils()
        {
            var context = new GameObject("[MonoBehaviourContext]").AddComponent<MonoBehaviourContext>();
            DontDestroyOnLoad(context.gameObject);
            
            Container.Bind<MonoBehaviourContext>().FromInstance(context).AsSingle().NonLazy();
        }

        private void BindAPI()
        {
            APIBinder apiBinder = new APIBinder(Container);
            apiBinder.Bind();
        }

        private void BindServices()
        {
            Container.Bind<ISceneLoaderService>().To<SceneLoaderService>().FromNew().AsSingle().NonLazy();
        }

        private void StartGame()
        {
            var monoBehaviourContext = Container.Resolve<MonoBehaviourContext>();
            var sceneLoader = Container.Resolve<ISceneLoaderService>();
            var apiEnvironment = Container.Resolve<IAPIEnvironmentService>();
            var localizationProvider = Container.Resolve<ILocalizationProvider>();
            
            new Boot(monoBehaviourContext, sceneLoader, apiEnvironment, localizationProvider);
        }
    }
}