using StateMachine;
using UnityEngine;

namespace Player.States
{
    [CreateAssetMenu(menuName = "States/Player/Idle")]
    public class PlayerIdleState : State<PlayerStateMachine>
    {
        public override void Enter(PlayerStateMachine parent)
        {
            base.Enter(parent);
            _runner.Move(Vector2.zero);
        }

        public override void Tick(float deltaTime)
        {            
        }

        public override void FixedTick(float fixedDeltaTime)
        {
        }

        public override void ChangeState()
        {
            if (_runner.Movement.sqrMagnitude != 0)
            {
                _runner.SetState(typeof(PlayerMove3DState));
            }
        }
    }
}
