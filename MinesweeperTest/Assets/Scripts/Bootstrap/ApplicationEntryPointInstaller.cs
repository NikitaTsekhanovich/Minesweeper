using Application.GameCore;
using Application.MenuCore;
using UnityEngine;
using Zenject;

namespace Bootstrap
{
    public class ApplicationEntryPointInstaller : MonoInstaller
    {
        [Header("Handlers")] 
        [SerializeField] private MenuHandler _menuHander;
        [SerializeField] private GameHandler _gameHandler;
        
        public override void InstallBindings()
        {
            _menuHander.ChangeState(true);
            _gameHandler.ChangeState(false);
            
            Container
                .Bind<MenuHandler>()
                .FromInstance(_menuHander)
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<GameHandler>()
                .FromInstance(_gameHandler)
                .AsSingle()
                .NonLazy();
        }
    }
}