using Game.Gameplay.Damage;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Gameplay.WinLose
{
    public class DeathHandler : MonoBehaviour
    {
        public event Action GameLost;

        [SerializeField] private Health _playerHealth;

        private void Start()
        {
            _playerHealth.HealthChanged += OnHealthChanged;
        }
        private void OnDestroy()
        {
            _playerHealth.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(int newHealth)
        {
            if(newHealth > 0)
            {
                return;
            }
            GameLost?.Invoke();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}