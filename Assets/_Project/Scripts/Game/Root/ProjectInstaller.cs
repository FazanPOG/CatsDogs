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
            
            Container.Resolve<ISceneLoaderService>().LoadGameplayScene();
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
    }
}