using Game.Gameplay.CharacterControllers;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Game.Animation
{
    /// <summary>
    /// I'm handling animation state through this simple
    /// event system in mechanim's stead.
    /// There's not much state to keep track of,
    /// this seems like the easiest way to handle
    /// animation for a game jam.
    /// </summary>
    public class PlayerAnimator : MonoBehaviour
    {
        /// <summary>
        /// No OdinInspector means that we unfortunately can't serialize interfaces.
        /// Since this game jam requires open-source code, that isn't an option.
        /// So we have to do this clunky solution.
        /// This is IPlayerAnimated.
        /// </summary>
        [Tooltip("Make sure this implement IPlayerAnimated")]
        [SerializeField] private Component _playerController;
        private IPlayerAnimated _player;
        [SerializeField] private Animator _animator;
        [SerializeField] private Light2D _light;
        [SerializeField] private Color _lightWhenRollAvailable;
        [SerializeField] private Color _lightWhenRollUnavailable;
        private void Start()
        {
            // I have to do this cast, since I can't use OdinInspector here.
            _player = _playerController as IPlayerAnimated;
            if(_player == null)
            {
                Debug.LogError("_playerController must implement IPlayerAnimated");
                return;
            }
            _player.StartedRunning += OnPlayerStartRunning;
            _player.StoppedRunning += OnPlayerStopRunning;
            _player.StartedRoll += OnPlayerRoll;
            _player.StoppedRoll += OnPlayerStopRoll;
            _player.RollCooldownEnded += OnRollCooldownEnded;
            _light.color = _lightWhenRollUnavailable;
        }
        private void OnDestroy()
        {
            _player.StartedRunning -= OnPlayerStartRunning;
            _player.StoppedRunning -= OnPlayerStopRunning;
            _player.StartedRoll -= OnPlayerRoll;
            _player.StoppedRoll -= OnPlayerStopRoll;
            _player.RollCooldownEnded -= OnRollCooldownEnded;
        }
        private void OnPlayerRoll()
        {
            _animator.Play("PlayerRoll");
            _light.color = _lightWhenRollUnavailable;
        }
        private void OnPlayerStopRoll()
        {
            string state = _player.IsRunning ? "PlayerRun" : "PlayerIdle";
            _animator.Play(state);
        }
        private void OnRollCooldownEnded()
        {
            _light.color = _lightWhenRollAvailable;
        }
        private void OnPlayerStartRunning()
        {
            _animator.Play("PlayerRun");
        }
        private void OnPlayerStopRunning()
        {
            _animator.Play("PlayerIdle");
        }
    }
}