using Application.GameFlowControllers;
using Domain.StateMachineBase.Properties;

namespace Application.GameCore.States
{
    public class RestartState : IEnterable
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly GameTimer _gameTimer;
        private readonly GameStateController _gameStateController;
        
        public RestartState(
            GameStateMachine gameStateMachine,
            GameTimer gameTimer,
            GameStateController gameStateController)
        {
            _gameStateMachine = gameStateMachine;
            _gameTimer = gameTimer;
            _gameStateController = gameStateController;
        }
        
        public void Enter()
        {
            _gameTimer.RestartTimer();
            _gameStateMachine.EnterIn<ExitState>();
            _gameStateController.SubscribeToActions();
            _gameStateMachine.EnterIn<LoadState>();
        }
    }
}