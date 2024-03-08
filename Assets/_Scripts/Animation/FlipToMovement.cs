using UnityEngine;

namespace Game.Animation
{
    public class FlipToMovement : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Rigidbody2D _rb;

        [SerializeField] private SpriteRenderer _spriteRenderer;


        #endregion

        #region Unity Methods

        private void Update()
        {
            if (Mathf.Abs(_rb.velocity.x) < 0.1f)
            {
                return;
            }
            _spriteRenderer.flipX = _rb.velocity.x < 0;
        }

        #endregion
    }
}