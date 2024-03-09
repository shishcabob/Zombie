using System;
using UnityEngine;

namespace Game.Gameplay.AI
{
    /// <summary>
    /// Takes into account the visibility of the target.
    /// This is a simple rules-based AI.
    /// No BTs, Utility AI, or anything like that here.
    /// </summary>
    public class NavigateToTarget : MonoBehaviour
    {
        #region Variables

        public event Action NoticedPlayer;
        public event Action StartedGoingToPlayer;

        [SerializeField] private Transform _eyes;
        [SerializeField] private Transform _target;
        [SerializeField] private LayerMask _visibleLayers;
        [SerializeField] private float maxVisibleDistance = 10;

        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private float _speed = 12f;

        [SerializeField] private SpriteRenderer _spriteRenderer;

        [SerializeField] private bool _lookingRight = true;
        [SerializeField] private float _flipInterval = 2;
        [SerializeField] private float _delayUntilChasePlayer = 0.5f;
        private float _timeSinceSeenPlayer = Mathf.Infinity;
        private float _timeSinceFlipped;
        private bool _couldSeeLastFrame = false;

        #endregion

        #region Fixed Update

        private void FixedUpdate()
        {
            bool playerVisible = LookForPlayer();
            FlipUpdate(playerVisible);
            if (!playerVisible)
            {
                _rb.velocity = Vector2.zero;
                _couldSeeLastFrame = false;
                _timeSinceSeenPlayer = 0;
                return;
            }
            if (!_couldSeeLastFrame)
            {
                NoticedPlayer?.Invoke();
            }
            if(_timeSinceSeenPlayer < _delayUntilChasePlayer)
            {
                _timeSinceSeenPlayer += Time.deltaTime;
                return;
            }
            var direction = (_target.position - transform.position).normalized;
            _rb.velocity = direction * _speed;
            _couldSeeLastFrame = true;
        }

        private void FlipUpdate(bool canSeePlayer)
        {
            if (canSeePlayer)
            {
                _timeSinceFlipped = 0;
                return;
            }
            _timeSinceFlipped += Time.deltaTime;
            if (_timeSinceFlipped < _flipInterval)
            {
                return;
            }
            Flip();
        }

        #endregion

        #region Helpers

        private void Flip()
        {
            _lookingRight = !_lookingRight;
            _spriteRenderer.flipX = !_lookingRight;
            _timeSinceFlipped = 0;
        }

        private bool LookForPlayer()
        {
            if (CheckDirectionWeAreFacing())
            {
                return true;
            }
            if (!_couldSeeLastFrame)
            {
                return false;
            }
            Flip();
            return CheckDirectionWeAreFacing();
        }

        private bool CheckDirectionWeAreFacing()
        {
            if (transform.position.x > _target.transform.position.x && _lookingRight)
            {
                // player is to our left, but we're looking right
                return false;
            }
            if (transform.position.x < _target.transform.position.x && !_lookingRight)
            {
                // player is to our right, but we're looking left
                return false;
            }
            var direction = _target.position - _eyes.position;
            if (!Physics2D.Raycast(_eyes.position, direction, maxVisibleDistance, _visibleLayers))
            {
                // we can't see the player
                return false;
            }
            return true;
        }

        #endregion
    }
}