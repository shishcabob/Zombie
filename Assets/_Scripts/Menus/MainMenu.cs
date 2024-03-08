using Game.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Menus
{
    public class MainMenu : MonoBehaviour
    {
        #region Variables
        [SerializeField] private SceneReference _levelOne;
        #endregion

        #region Unity Methods

        #endregion

        #region Methods
        public void OnPlayPressed()
        {
            SceneManager.LoadScene(_levelOne);
        }
        #endregion
    }
}