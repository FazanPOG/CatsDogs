using UnityEngine;

namespace _Project.Gameplay
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private AnimalView _smallCatViewPrefab;
        [SerializeField] private AnimalView _bigCatViewPrefab;
        [SerializeField] private AnimalView _smallDogCatViewPrefab;
        [SerializeField] private AnimalView _bigDogCatViewPrefab;

        public void Init()
        {
            
        }
    }
}