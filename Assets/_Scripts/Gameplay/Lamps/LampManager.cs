using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Lamps
{
    public class LampManager : MonoBehaviour
    {
        #region Variables

        public static LampManager Instance { get; private set; }

        public event Action<Lamp> LampEnabled;
        public event Action AllLampsOn;
        public int Count => _lamps.Count;

        private HashSet<Lamp> _lamps = new();

        #endregion

        #region Unity Methods

        private void Awake()
        {
            #region Singleton
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            #endregion
        }

        private void OnDestroy()
        {
            foreach(var lamp in _lamps)
            {
                lamp.TurnedOn -= OnLampTurnedOn;
            }
        }

        #endregion

        #region Methods

        public void RegisterLamp(Lamp lamp)
        {
            lamp.TurnedOn += OnLampTurnedOn;
            _lamps.Add(lamp);
        }
        private void OnLampTurnedOn(Lamp lamp)
        {
            _lamps.Remove(lamp);
            LampEnabled?.Invoke(lamp);
            if(_lamps.Count > 0)
            {
                return;
            }
            AllLampsOn?.Invoke();
        }

        #endregion
    }
}