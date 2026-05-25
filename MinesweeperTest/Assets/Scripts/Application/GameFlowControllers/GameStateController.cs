using System;
using Application.GameCore;
using Application.GameCore.States;
using Application.GameField;

namespace Application.GameFlowControllers
{
    public class GameStateController : IDisposable
    {
        private readonly Field _field;
        private readonly GameStateMachine _gameStateMachine;
        
        public event Action OnGameWin;
        public event Action OnGameLose;
        
        public GameStateController(Field field, GameStateMachine gameStateMachine)
        {
            _field = field;
            _gameStateMachine = gameStateMachine;
            
            _field.OnFindBomb += SetLoseState;
            _field.OnClearedBombs += SetWinState;
        }
        
        public void Dispose()
        {
            _field.OnFindBomb -= SetLoseState;
            _field.OnClearedBombs -= SetWinState;
        }
        
        private void SetWinState()
        {
            OnGameWin?.Invoke();
            _gameStateMachine.EnterIn<PauseState>();
        }

        private void SetLoseState()
        {
            OnGameLose?.Invoke();
            _gameStateMachine.EnterIn<PauseState>();
        }
    }
}