using Game.SceneManagement;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game.Gameplay.Lamps;

namespace Game.Gameplay.WinLose
{
    public class WinHandler : MonoBehaviour
    {
        #region Variables

        public event Action GameWon;

        [SerializeField] private SceneReference _nextLevel;

        #endregion

        #region Unity Methods

        private void Start()
        {
            LampManager.Instance.AllLampsOn += OnAllLampsOn;
        }
        private void OnDestroy()
        {
            LampManager.Instance.AllLampsOn -= OnAllLampsOn;
        }

        #endregion

        #region Methods

        private void OnAllLampsOn()
        {
            GameWon?.Invoke();
            SceneManager.LoadScene(_nextLevel);
        }

        #endregion
    }
}