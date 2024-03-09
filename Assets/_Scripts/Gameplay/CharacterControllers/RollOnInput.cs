using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Gameplay.CharacterControllers
{
    public class RollOnInput : MonoBehaviour
    {
        public event Action StartedRunning;
        public event Action StoppedRunning;

        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private float _speed = 3f;

        private Vector2 moveInput;

        private const float MIN_RUNNING_SPEED = 0.1f;

        private void FixedUpdate()
        {
            FireEvents();
            _rb.velocity = moveInput * _speed;
        }

        public void OnMovementInput(InputAction.CallbackContext ctx)
        {
            moveInput = ctx.ReadValue<Vector2>();
        }

        private void FireEvents()
        {
            var xVelocity = Mathf.Abs(_rb.velocity.x);
            var sqrMoveMagnitude = moveInput.sqrMagnitude;

            if (xVelocity < MIN_RUNNING_SPEED
                && sqrMoveMagnitude > 0)
            {
                StartedRunning?.Invoke();
                return;
            }
            if (xVelocity > MIN_RUNNING_SPEED
                && sqrMoveMagnitude == 0)
            {
                StoppedRunning?.Invoke();
                return;
            }
        }
    }
}