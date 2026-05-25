using Application.GameCore;
using Application.MenuCore;
using UnityEngine;
using Zenject;

namespace Presentation
{
    public class GameMenuView : MonoBehaviour
    {
        [SerializeField] private GameObject _gameMenuWindow;
        
        [Inject] private GameHandler _gameHandler;
        [Inject] private MenuHandler _menuHandler;
        
        public void Pause()
        {
            _gameMenuWindow.SetActive(true);
            _gameHandler.ChangePauseState(true);
        }

        public void Resume()
        {
            _gameHandler.ChangePauseState(false);
            _gameMenuWindow.SetActive(false);
        }

        public void Restart()
        {
            _gameHandler.RestartGame();
            _gameMenuWindow.SetActive(false);
        }

        public void BackToMenu()
        {
            _gameHandler.ChangeState(false);
            _menuHandler.ChangeState(true);
            _gameMenuWindow.SetActive(false);
        }
    }
}