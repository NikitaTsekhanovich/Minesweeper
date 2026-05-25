using System;
using Application.GameFlowControllers;
using UnityEngine;

namespace Presentation.GameFlowView
{
    public class GameStateControllerView : MonoBehaviour, IDisposable
    {
        [SerializeField] private GameObject _winWindow;
        [SerializeField] private GameObject _loseWindow;
        [SerializeField] private GameObject _panel;
        
        private GameStateController _gameStateController;

        public void Constructor(GameStateController gameStateController)
        {
            _gameStateController = gameStateController;

            _gameStateController.OnGameWin += ShowWinWindow;
            _gameStateController.OnGameLose += ShowLoseWindow;
        }

        private void OnDestroy()
        {
            Dispose();
        }
        
        public void Dispose()
        {
            if (_gameStateController != null)
            {
                _gameStateController.OnGameWin -= ShowWinWindow;
                _gameStateController.OnGameLose -= ShowLoseWindow;
            }

            _gameStateController = null;
            Restart();
        }

        public void Restart()
        {
            _winWindow.SetActive(false);
            _loseWindow.SetActive(false);
            _panel.SetActive(false);
        }

        private void ShowWinWindow()
        {
            _winWindow.SetActive(true);
            _panel.SetActive(true);
        }

        private void ShowLoseWindow()
        {
            _loseWindow.SetActive(true);
            _panel.SetActive(true);
        }
    }
}