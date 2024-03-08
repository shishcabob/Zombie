using UnityEngine;

namespace Game.Gameplay.Damage
{
    public class HitBox : MonoBehaviour
    {
        [SerializeField] private int Damage = 1;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            IDamageable damageable;
            if (!collision.transform.TryGetComponent<IDamageable>(out damageable))
            {
                return;
            }
            damageable.DealDamage(Damage);
        }
    }
}