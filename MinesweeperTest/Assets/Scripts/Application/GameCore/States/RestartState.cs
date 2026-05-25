using Application.GameFlowControllers;
using Domain.StateMachineBase.Properties;
using Presentation.GameFieldViews;
using Presentation.GameFlowView;

namespace Application.GameCore.States
{
    public class RestartState : IEnterable
    {
        private readonly GameTimer _gameTimer;
        private readonly FieldView _fieldView;
        private readonly GameStateControllerView _gameStateControllerView;
        
        public RestartState(
            GameTimer gameTimer,
            FieldView fieldView,
            GameStateControllerView gameStateControllerView)
        {
            _gameTimer = gameTimer;
            _fieldView = fieldView;
            _gameStateControllerView = gameStateControllerView;
        }
        
        public void Enter()
        {
            _gameTimer.UpdateValue(0f);
            _fieldView.RestartFieldView();
            _gameStateControllerView.Restart();
        }
    }
}