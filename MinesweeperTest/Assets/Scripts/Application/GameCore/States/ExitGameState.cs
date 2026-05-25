using Application.GameField;
using Application.GameFlowControllers;
using Domain.Properties;
using Presentation.GameFieldViews;
using Presentation.GameFlowView;

namespace Application.GameCore.States
{
    public class ExitState : IEnterable
    {
        private readonly Field _field;
        private readonly GameStateController _gameStateController;
        private readonly FieldView _fieldView;
        private readonly GameTimerView _gameTimerView;
        private readonly GameStateControllerView _gameStateControllerView;
        
        public ExitState(
            Field field, 
            GameStateController gameStateController,
            FieldView fieldView,
            GameTimerView gameTimerView,
            GameStateControllerView gameStateControllerView)
        {
            _field = field;
            _gameStateController = gameStateController;
            _fieldView = fieldView;
            _gameTimerView = gameTimerView;
            _gameStateControllerView = gameStateControllerView;
        }
        
        public void Enter()
        {
            _field.Dispose();
            _gameStateController.Dispose();
            _fieldView.Dispose();
            _gameTimerView.Dispose();
            _gameStateControllerView.Dispose();
        }
    }
}