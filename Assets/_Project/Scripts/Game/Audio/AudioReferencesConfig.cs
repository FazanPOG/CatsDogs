using UnityEngine;

namespace _Project.Audio
{
    [CreateAssetMenu(menuName = "_Project/Configs/Audio/AudioReferences")]
    public class AudioReferencesConfig : ScriptableObject
    {
        [SerializeField] private AudioClip _backgroundMusic;
        [SerializeField] private AudioClip _clickClip;
        [SerializeField] private AudioClip _plopClip;
        [SerializeField] private AudioClip _fireworksClip;
        [SerializeField] private AudioClip _catClip;
        [SerializeField] private AudioClip _dogClip;

        public AudioClip BackgroundMusic => _backgroundMusic;

        public AudioClip ClickClip => _clickClip;

        public AudioClip PlopClip => _plopClip;

        public AudioClip FireworksClip => _fireworksClip;

        public AudioClip CatClip => _catClip;

        public AudioClip DogClip => _dogClip;
    }
}