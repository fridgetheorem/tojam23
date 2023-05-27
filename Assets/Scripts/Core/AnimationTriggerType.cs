/*
Reference: BerserkPixel: https://itch.io/blog/536178/unity-state-machine-2d-top-down-game
*/

namespace StateMachine
{
    // whatever could be called from the animation timeline
    public enum AnimationTriggerType
    {
        EnemyDamaged,
        PlayFootstepSound
    }
}