using Cinemachine;
using UnityEngine;

namespace _Project.Gameplay
{
    public class CameraSystem : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        
        public void Follow(Transform target)
        {
            _virtualCamera.Follow = target;
            _virtualCamera.LookAt = target;
        }
    }
}