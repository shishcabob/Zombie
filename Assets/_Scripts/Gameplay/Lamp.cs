using System;
using UnityEngine;

namespace Game.Gameplay
{
    public class Lamp : MonoBehaviour
    {
        [SerializeField] private GameObject _lightSource;

        public event Action<Lamp> TurnedOn;

        private void Start()
        {
            LampManager.Instance.RegisterLamp(this);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(!collision.transform.CompareTag("Player"))
            {
                return;
            }
            _lightSource.SetActive(true);
            TurnedOn?.Invoke(this);
        }
    }
}