using Game.Gameplay;
using TMPro;
using UnityEngine;

namespace Game.Animation
{
    public class LampCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counter;

        private void Start()
        {
            LampManager.Instance.LampEnabled += OnLampEnabled;
        }

        private void OnDestroy()
        {
            LampManager.Instance.LampEnabled -= OnLampEnabled;
        }

        private void OnLampEnabled(Lamp lamp)
        {
            _counter.text = $"{LampManager.Instance.Count}";
        }
    }
}