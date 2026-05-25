using Application.GameCore;
using Application.MenuCore;
using UnityEngine;
using Zenject;

namespace Presentation
{
    public class MenuView : MonoBehaviour
    {
        [Inject] private MenuHandler _menuHandler;
        [Inject] private GameHandler _gameHandler;
        
        public void Play()
        { 
            _menuHandler.ChangeState(false);
            _gameHandler.ChangeState(true);
        }
    }
}