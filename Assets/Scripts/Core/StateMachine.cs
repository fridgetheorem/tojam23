using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
Reference: BerserkPixel: https://itch.io/blog/536178/unity-state-machine-2d-top-down-game
*/

namespace StateMachine
{
    public abstract class StateMachine<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private List<State<T>> _states;

        [Header("DEBUG")] 
        [SerializeField] private bool _debug = true;

        private State<T> _activeState;

        private T _parent;

        protected virtual void Awake()
        {
            _parent = GetComponent<T>();
        }

        protected virtual void Start()
        {
            if (_states.Count <= 0) return;

            SetState(_states[0]);
        }

        public void SetState(State<T> newStateType)
        {
            _activeState?.Exit();
            _activeState = newStateType;
            _activeState?.Enter(_parent);
        }

        public void SetState(Type newStateType)
        {
            var newState = _states.FirstOrDefault(s => s.GetType() == newStateType);
            if (newState)
            {
                SetState(newState);
            }
        }

        protected virtual void Update()
        {
            _activeState?.Tick(Time.deltaTime);
            _activeState?.ChangeState();
        }

        /// <summary>
        /// Can be called from the Animation Timeline. This will propagate the AnimationTriggerType
        /// to the current active state.
        /// </summary>
        /// <param name="triggerType"></param>
        private void SetAnimationTriggerEvent(AnimationTriggerType triggerType)
        {
            _activeState?.AnimationTriggerEvent(triggerType);
        }

        private void FixedUpdate()
        {
            _activeState?.FixedTick(Time.fixedDeltaTime);
        }

        private void OnGUI()
        {
            if (!_debug) return;

            var content = _activeState != null ? _activeState.name : "(no active state)";
            GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
        }
    }
}