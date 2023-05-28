using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player.Input
{
    public class PlayerInput : MonoBehaviour, GameControls.IPlayerActions
    {
        public event UnityAction<Vector2> MovementEvent = delegate { };
        public event UnityAction<bool> InteractEvent = delegate { };

        private GameControls _playerActions;

        private void OnEnable()
        {
            if (_playerActions == null)
            {
                _playerActions = new GameControls();
                _playerActions.Player.SetCallbacks(this);
		// _playerActions.UI.SetCallbacks(this);
            }

            EnableGameplayInput();
        }

        private void OnDisable()
        {
            DisableAllInput();
        }

        public void DisableAllInput()
        {
            _playerActions.Player.Disable();
        }

        public void EnableGameplayInput()
        {
            if (_playerActions.Player.enabled) return;

            // _playerActions.UI.Disable();
            _playerActions.Player.Enable();
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            MovementEvent?.Invoke(context.ReadValue<Vector2>());
            Debug.Log(context.ReadValue<Vector2>());
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            InteractEvent?.Invoke(context.ReadValue<bool>());
        }
    }
}