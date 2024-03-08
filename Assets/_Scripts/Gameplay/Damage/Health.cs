using System;
using UnityEngine;

namespace Game.Gameplay.Damage
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _value = 1;
        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                HealthChanged?.Invoke(value);
            }
        }
        public event Action<int> HealthChanged;
    }
}