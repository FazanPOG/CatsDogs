using UnityEngine;

namespace _Project.Gameplay
{
    [RequireComponent(typeof(BoxCollider))]
    public class EndLevelTrigger : MonoBehaviour
    {
        private void Awake() => GetComponent<BoxCollider>().isTrigger = true;
    }
}