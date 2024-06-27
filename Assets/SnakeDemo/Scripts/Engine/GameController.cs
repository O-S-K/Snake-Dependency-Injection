using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour, IGameController
{
    public enum GameState
    {
        Start,
        InGame,
        EndGame
    }
    public GameState currentState ;

    [Inject] private ISnake snake;
    [Inject] private IGrid grid;
    
    public void Init()
    {
        currentState = GameState.Start;
        snake.Init();
        grid.Init();
    }

    public void StartGame()
    {
        currentState = GameState.InGame;
        Debug.Log("Game started!");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
        
        UpdateGame();
    }

    public void UpdateGame()
    {
        if (IsInGame())
        {
            snake.GetInput();
            snake.CheckMove();
        }
    }

    public void EndGame()
    {   
        currentState = GameState.EndGame;
        DIContainer.Resolve<UIManager>().Show<ResultDialog>();
        Debug.Log("Game ended!");
    }

    public bool IsInGame()
    {
        return currentState == GameState.InGame;
    }
}