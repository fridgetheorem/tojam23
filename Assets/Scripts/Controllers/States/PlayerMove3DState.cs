using StateMachine;
using UnityEngine;

namespace Player.States
{
    [CreateAssetMenu(menuName = "States/Player/Move 3D")]
    public class PlayerMove3DState : State<PlayerStateMachine>
    {
        [SerializeField, Range(250f, 500f)] private float _speed = 300f;

        private Vector3 _playerInput;

        public override void Tick(float deltaTime)
        {
            _playerInput = new Vector3(_runner.Movement.x, 0, _runner.Movement.y);
        }

        public override void FixedTick(float fixedDeltaTime)
        {
            _runner.Move(_playerInput * (_speed * fixedDeltaTime));
        }

        public override void ChangeState()
        {
            if (_playerInput == Vector3.zero)
            {
                _runner.SetState(typeof(PlayerIdleState));
            }
        }
    }
}