using System;
using System.Collections.Generic;
using Application.Configs;
using Application.GameCore.States;
using Application.GameField;
using Application.GameFlowControllers;
using Domain;
using Domain.StateMachineBase;
using Presentation.GameFieldViews;
using Presentation.GameFlowView;

namespace Application.GameCore
{
    public class GameStateMachine : StateMachine
    {
        public GameStateMachine(
            FieldView fieldView,
            GameTimerView gameTimerView,
            GameStateControllerView gameStateControllerView,
            GameFieldConfig gameFieldConfig)
        {
            var gameTimer = new GameTimer();
            var field = new Field(gameFieldConfig, this);
            var gameStateController = new GameStateController(field, this);
            
            States = new Dictionary<Type, object>
            {
                [typeof(LoadState)] = new LoadState(
                    fieldView,
                    gameTimerView,
                    gameStateControllerView,
                    gameTimer,
                    gameStateController,
                    field),
                [typeof(WaitingClickState)] = new WaitingClickState(),
                [typeof(LoopState)] = new LoopState(gameTimer),
                [typeof(PauseState)] = new PauseState(this),
                [typeof(RestartState)] = new RestartState(this, gameTimer, gameStateController),
                [typeof(ExitState)] = new ExitState(
                    field, 
                    gameStateController,
                    fieldView,
                    gameTimerView,
                    gameStateControllerView)
            };
        }
    }
}