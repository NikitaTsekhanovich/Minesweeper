using Application.Configs;
using Application.GameCore.States;
using Presentation.GameFieldViews;
using Presentation.GameFlowView;
using UnityEngine;

namespace Application.GameCore
{
    public class GameHandler : MonoBehaviour
    {
        [Header("Views")]
        [SerializeField] private FieldView _fieldView;
        [SerializeField] private GameTimerView _gameTimerView;
        [SerializeField] private GameStateControllerView _gameStateControllerView;
        
        [Header("Configs")]
        [SerializeField] private GameFieldConfig _gameFieldConfig;
        
        private GameStateMachine _gameStateMachine;

        private void Update()
        {
            _gameStateMachine?.UpdateSystem();
        }

        public void ChangeState(bool state)
        {
            if (state)
            {
                InitGameStateMachine();
                _gameStateMachine.EnterIn<LoadState>();
                _gameStateMachine.EnterIn<WaitingClickState>();
            }
            else
            {
                _gameStateMachine?.EnterIn<ExitState>();
                _gameStateMachine = null;
            }
            
            gameObject.SetActive(state);
        }

        public void ChangePauseState(bool isPause)
        {
            if (isPause)
            {
                _gameStateMachine.EnterIn<PauseState>();
            }
            else
            {
                _gameStateMachine.ExitIn<PauseState>();
            }
        }

        public void RestartGame()
        {
            _gameStateMachine.EnterIn<RestartState>();
            _gameStateMachine.EnterIn<WaitingClickState>();
        }
        
        private void InitGameStateMachine()
        {
            _gameStateMachine = new GameStateMachine(
                _fieldView,
                _gameTimerView,
                _gameStateControllerView,
                _gameFieldConfig);
        }
    }
}