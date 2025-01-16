using _Project.API;
using _Project.Audio;
using _Project.Game;
using _Project.Utility;
using UnityEngine;
using Zenject;

namespace _Project.Root
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private AudioReferencesConfig _audioReferencesConfig;
        
        public override void InstallBindings()
        {
            BindUtils();
            BindAPI();
            BindServices();
            BindAudio();
            
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
        
        private void BindAudio()
        {
            var audioSource = new GameObject("[Audio]").AddComponent<AudioSource>();
            DontDestroyOnLoad(audioSource.gameObject);
            
            AudioPlayer audioPlayer = new AudioPlayer(audioSource, _audioReferencesConfig);
            AudioPlayer backgroundMusicPlayer = new AudioPlayer(audioSource, _audioReferencesConfig);

            backgroundMusicPlayer.PlayBackgroundMusic();
            
            Container.Bind<AudioPlayer>().FromInstance(audioPlayer).AsSingle().NonLazy();
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