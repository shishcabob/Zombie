using Game.SceneManagement;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Gameplay
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