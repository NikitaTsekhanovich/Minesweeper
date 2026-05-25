using Application.GameField;
using Application.GameFlowControllers;
using Domain.StateMachineBase.Properties;
using Presentation.GameFieldViews;
using Presentation.GameFlowView;

namespace Application.GameCore.States
{
    public class LoadState : IEnterable
    {
        private readonly FieldView _fieldView;
        private readonly GameTimerView _gameTimerView;
        private readonly GameStateControllerView _gameStateControllerView;
        private readonly GameTimer _gameTimer;
        private readonly GameStateController _gameStateController;
        private readonly Field _field;
        
        public LoadState(
            FieldView fieldView,
            GameTimerView gameTimerView,
            GameStateControllerView gameStateControllerView,
            GameTimer gameTimer,
            GameStateController gameStateController,
            Field field)
        {
            _fieldView = fieldView;
            _gameTimerView = gameTimerView;
            _gameStateControllerView = gameStateControllerView;
            _gameTimer = gameTimer;
            _gameStateController = gameStateController;
            _field = field;
        }
        
        public void Enter()
        {
            _gameStateControllerView.Constructor(_gameStateController);
            
            _gameTimerView.Constructor(_gameTimer);
            
            _fieldView.Constructor(_field);
            _fieldView.InitGameField();
        }
    }
}