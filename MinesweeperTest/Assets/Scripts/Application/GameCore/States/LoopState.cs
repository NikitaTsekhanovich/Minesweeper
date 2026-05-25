using Application.GameFlowControllers;
using Domain.StateMachineBase.Properties;

namespace Application.GameCore.States
{
    public class LoopState : ICanUpdate
    {
        private readonly GameTimer _gameTimer;
        
        public LoopState(GameTimer gameTimer)
        {
            _gameTimer = gameTimer;
        }
        
        public void UpdateSystem()
        {
            _gameTimer.UpdateTimer();
        }
    }
}