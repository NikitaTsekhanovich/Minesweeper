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
            UpdateValue(_currentTime);
        }

        public void UpdateValue(float value)
        {
            _currentTime = value;
            OnTimeChanged?.Invoke(_currentTime);
        }
    }
}