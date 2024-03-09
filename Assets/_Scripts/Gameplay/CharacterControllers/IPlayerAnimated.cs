using System;

namespace Game.Gameplay.CharacterControllers
{
    public interface IPlayerAnimated
    {
        event Action StartedRunning;
        event Action StoppedRunning;
        event Action StartedRoll;
        event Action StoppedRoll;
        event Action RollCooldownEnded;
        bool IsRunning { get; }
    }
}
