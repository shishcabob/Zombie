using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Gameplay
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private float _speed = 3f;

        private Vector2 moveInput;

        private void FixedUpdate()
        {
            _rb.velocity = moveInput * _speed;
        }

        public void OnMovementInput(InputAction.CallbackContext ctx)
        {
            moveInput = ctx.ReadValue<Vector2>();
        }
    }
}