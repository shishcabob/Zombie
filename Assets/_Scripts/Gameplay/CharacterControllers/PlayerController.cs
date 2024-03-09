using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Gameplay.CharacterControllers
{
    /// <summary>
    /// The character controller for this game is pretty simple,
    /// We only move on the x/y axis and we have a dodge roll.
    /// This means one small script is more than enough, no need to over-engineer it.
    /// 
    /// Also, since this is a game jam, time is too valuable to waste on creating a more
    /// robust controller. I'll save that for my long-term projects.
    /// </summary>
    public class PlayerController : MonoBehaviour, IPlayerAnimated
    {
        #region Variables

        #region Events

        public event Action StartedRunning;
        public event Action StoppedRunning;
        public event Action StartedRoll;
        public event Action StoppedRoll;
        public event Action RollCooldownEnded;

        #endregion

        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _rollSpeed = 18f;
        [SerializeField] private float _rollDuration = 0.4f;
        [SerializeField] private float _rollCooldown = 1.5f;

        private Vector2 moveInput;
        
        private bool _rollInputReceived = false;
        private bool _rolling = false;
        private float _timeSinceLastRoll = 0;
        private Vector2 _rollDirection;
        private float _timeSinceRollStart = 0;

        private const float MIN_RUNNING_SPEED = 0.1f;

        public bool IsRunning => _rb.velocity.x > MIN_RUNNING_SPEED && !_rolling;
        private bool _rollOnCooldown = true;

        #endregion

        #region Unity Methods

        private void FixedUpdate()
        {
            bool rollOnCooldownThisFrame = _timeSinceLastRoll < _rollCooldown;
            if (_rollOnCooldown && !rollOnCooldownThisFrame)
            {
                RollCooldownEnded?.Invoke();
            }
            _rollOnCooldown = rollOnCooldownThisFrame;
            if (_rolling || (_rollInputReceived && !_rollOnCooldown))
            {
                HandleRolling();
                return;
            }
            _timeSinceLastRoll += Time.deltaTime;
            _rollInputReceived = false; // could consider adding coyote time to this input
            FireRunningEvents();
            _rb.velocity = moveInput * _speed;
        }

        #endregion

        #region Input Receivers

        public void OnMovementInput(InputAction.CallbackContext ctx)
        {
            moveInput = ctx.ReadValue<Vector2>();
        }

        public void OnRollInput(InputAction.CallbackContext ctx)
        {
            if (!ctx.performed)
            {
                return;
            }
            _rollInputReceived = true;
        }

        #endregion

        #region Helpers

        private void FireRunningEvents()
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

        #region Rolling

        /// <summary>
        /// Overrides velocity (player movement doesn't matter during rolls)
        /// </summary>
        private void HandleRolling()
        {
            if (_timeSinceRollStart > _rollDuration)
            {
                _timeSinceRollStart = 0;
                _rollDuration = 0;
                _rolling = false;
                StoppedRoll?.Invoke();
                return;
            }
            if (_rolling)
            {
                _timeSinceRollStart += Time.deltaTime; // constant in fixedupdate
                SetRollSpeed();
                return;
            }
            if (!_rollInputReceived)
            {
                return;
            }
            // start rolling
            _timeSinceLastRoll = 0f;
            _rollInputReceived = false;
            _rolling = true;
            if(moveInput.sqrMagnitude > 0)
            {
                _rollDirection = moveInput;
            }
            else if(transform.rotation.y == 0)
            {
                _rollDirection = Vector3.right;
            }
            else
            {
                _rollDirection = Vector3.left;
            }
            SetRollSpeed();
            StartedRoll?.Invoke();
            return;
        }
        private void SetRollSpeed()
        {
            _rb.velocity = _rollDirection * _rollSpeed;
        }

        #endregion

        #endregion
    }
}