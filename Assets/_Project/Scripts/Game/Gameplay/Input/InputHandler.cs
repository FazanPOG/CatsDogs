using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Gameplay
{
    public class InputHandler
    {
        public Vector2 MoveDirection { get; private set; }
        public bool IsPressing { get; private set; }

        public InputHandler()
        {
            PlayerInputActions playerInputActions = new PlayerInputActions();
            playerInputActions.Enable();
            
            playerInputActions.Movement.Delta.performed += HandleDelta;
            playerInputActions.Movement.Delta.canceled += HandleDelta;
            
            playerInputActions.Movement.Press.performed += HandleClick;
            playerInputActions.Movement.Press.canceled += HandleClick;
        }

        private void HandleClick(InputAction.CallbackContext callbackContext)
        {
            IsPressing = callbackContext.ReadValue<float>() > 0;
        }

        private void HandleDelta(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.canceled)
                MoveDirection = Vector2.zero;
            else
                MoveDirection = callbackContext.ReadValue<Vector2>();
        }
    }
}
