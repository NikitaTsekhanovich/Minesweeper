using System;
using UnityEngine;

namespace Application.GameFlowControllers
{
    public class GameTimer
    { 
        private float _currentTime;
        
        public event Action<float> OnTimeChanged;
        
        public void UpdateTimer()
        {
            _currentTime += Time.deltaTime;
            OnTimeChanged?.Invoke(_currentTime);
        }

        public void RestartTimer()
        {
            _currentTime = 0f;
        }
    }
}