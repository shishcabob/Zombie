using UnityEngine;

namespace Game.Animation
{
    public class FlipToMovement : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Rigidbody2D _rb;

        [SerializeField] private SpriteRenderer _spriteRenderer;

        [Tooltip("Should we flip the sprite or the whole transform?")]
        [SerializeField] private bool flipSpriteOnly = false;


        #endregion

        #region Unity Methods

        private void Update()
        {
            if (Mathf.Abs(_rb.velocity.x) < 0.1f)
            {
                return;
            }
            if (flipSpriteOnly)
            {
                _spriteRenderer.flipX = _rb.velocity.x < 0;
                return;
            }
            float newY = _rb.velocity.x < 0 ? 180 : 0;
            transform.rotation = Quaternion.Euler(new Vector3(0, newY, 0));
        }

        #endregion
    }
}