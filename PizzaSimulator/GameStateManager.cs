using PizzaSimulator.Content.Components;
using PizzaSimulator.Content.Entities;
using PizzaSimulator.Content.UI;
using PizzaSimulator.Content.World;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSimulator
{
    public class GameStateManager : Singleton<GameStateManager>
    {
        protected void StartGame()
        {
            UIManager.Instance.Elements.Clear();

            GameLoop.MyPlayer = new Player();
            GameLoop.World = new GameWorld();
            EntityManager.Instance.Entities.Clear();

            UIManager.Instance.AddElement(new UIStats());
        }

        protected void GoToMenu()
        {
            UIManager.Instance.Elements.Clear();
            EntityManager.Instance.Entities.Clear();
            GameLoop.MyPlayer = null;
            GameLoop.World = null;
            UIManager.Instance.AddElement(new UIMainMenu());
        }

        public void SwitchState(GameState state)
        {
            CurrentGameState = state;

            switch(CurrentGameState)
            {
                case GameState.GameLoad:
                    break;
                case GameState.GameMenu:
                    GoToMenu();
                    break;
                case GameState.StartPLay:
                    StartGame();
                    break;
            }
        }

        public GameState CurrentGameState { get; private set; }
    }
}
