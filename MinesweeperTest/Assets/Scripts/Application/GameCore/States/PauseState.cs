using System;
using Domain.Properties;
using UnityEngine;

namespace Application.GameCore.States
{
    public class PauseState : IEnterable, IExitable
    {
        private readonly GameStateMachine _gameStateMachine;
        
        private Type _previousState;

        public PauseState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _previousState = _gameStateMachine.PreviousStateType;
        }

        public void Exit()
        {
            var methodInfo = typeof(GameStateMachine).GetMethod("EnterIn");
            
            if (methodInfo != null)
            {
                var genericMethod = methodInfo.MakeGenericMethod(_previousState);
                genericMethod.Invoke(_gameStateMachine, new object[] { });
            }
            else
            {
                Debug.LogError("Error getting method via reflection.");
                throw new NullReferenceException();
            }
        }
    }
}