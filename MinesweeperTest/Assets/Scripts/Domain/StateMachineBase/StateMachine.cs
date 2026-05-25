using System;
using System.Collections.Generic;
using Domain.StateMachineBase.Properties;

namespace Domain.StateMachineBase
{
    public class StateMachine
    {
        private ICanUpdate _updateState;
        
        protected Dictionary<Type, object> States = new ();
        
        public Type PreviousStateType { get; private set; }
        
        public void EnterIn<TState>() 
            where TState : class
        {
            if (States.TryGetValue(typeof(TState), out var state))
            {
                var enterState = state as IEnterable;
                _updateState = state as ICanUpdate;
                
                enterState?.Enter();
                
                PreviousStateType = typeof(TState);
            }
        }

        public void ExitIn<TState>()
            where TState : class
        {
            if (States.TryGetValue(typeof(TState), out var state))
            {
                var currentExitState = state as IExitable;
                currentExitState?.Exit();
            }
        }

        public void UpdateSystem()
        {
            _updateState?.UpdateSystem();
        }
    }
}
