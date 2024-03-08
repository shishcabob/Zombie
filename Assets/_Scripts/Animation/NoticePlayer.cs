using Game.Gameplay;
using System.Collections;
using UnityEngine;

namespace Game.Animation
{
    public class NoticePlayer : MonoBehaviour
    {
        #region Variables

        [SerializeField] private NavigateToTarget _navigation;
        [SerializeField] private GameObject _noticeSymbol;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _navigation.NoticedPlayer += OnNoticedPlayer;
        }
        private void OnDestroy()
        {
            _navigation.NoticedPlayer -= OnNoticedPlayer;
        }

        #endregion

        #region Methods

        private void OnNoticedPlayer()
        {
            StartCoroutine(NoticeAnimation());
        }
        private IEnumerator NoticeAnimation()
        {
            _noticeSymbol.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _noticeSymbol.SetActive(false);
        }

        #endregion
    }
}