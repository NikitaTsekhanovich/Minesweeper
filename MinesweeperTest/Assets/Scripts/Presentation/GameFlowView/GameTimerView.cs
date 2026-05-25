using System;
using Application.GameFlowControllers;
using TMPro;
using UnityEngine;

namespace Presentation.GameFlowView
{
    public class GameTimerView : MonoBehaviour, IDisposable
    {
        [SerializeField] private TMP_Text _timerText;

        private GameTimer _gameTimer;
        
        public void Constructor(GameTimer gameTimer)
        {
            _gameTimer = gameTimer;
            _timerText.text = "Time: 0.0";
            
            _gameTimer.OnTimeChanged += UpdateTimer;
        }
        
        private void OnDestroy()
        {
            Dispose();
        }

        private void UpdateTimer(float time)
        {
            _timerText.text = $"Time: {time:0.0}";
        }

        public void Dispose()
        {
            if (_gameTimer != null)
            {
                _gameTimer.OnTimeChanged -= UpdateTimer;
            }
            
            _gameTimer = null;
        }
    }
}