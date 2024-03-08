using UnityEngine;

namespace Game.Gameplay.Damage
{
    public class HurtBox : MonoBehaviour, IDamageable
    {
        [SerializeField] private Health _health;

        void IDamageable.DealDamage(int damage)
        {
            _health.Value -= damage;
        }
    }
}