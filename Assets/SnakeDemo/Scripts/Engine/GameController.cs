using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class GameController : MonoBehaviour, IGameController
{
    public enum GameState
    {
        Start,
        InGame,
        EndGame
    }

    public enum EndGameType
    {
        P1Win,
        P2Win,
        Draw
    }


    public GameState currentState;


    [Inject] private IGrid grid;
    [Inject] private DataSO dataSO;
    [Inject] private P1Snake p1Snake;
    [Inject] private P2Snake p2Snake;

    protected int currentLevel = 0;
    protected float timeUp;
    protected bool isTimeUp;


    public void Init()
    {
        currentState = GameState.Start;

        p1Snake.Init(dataSO.SnakeColors[Random.Range(0, 2)]);
        p2Snake.Init(dataSO.SnakeColors[Random.Range(2, 4)]);
        grid.Init();
    }

    public void StartGame()
    {
        currentState = GameState.InGame;
        Debug.Log("Game started!");
    }

    public void AddScore(int type)
    {
        if (type == 0)
        {
            GameData.ScorePlayer++;
        }
        else
        {
            GameData.ScoreEnemy++;
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        UpdateGame();
    }


    public bool IsInGame()
    {
        return currentState == GameState.InGame;
    }

    public void UpdateGame()
    {
        if (IsInGame())
        {
            timeUp += Time.deltaTime;

            if (currentLevel >= dataSO.LevelUps.Length)
                return;

            if (Mathf.RoundToInt(timeUp) >= dataSO.LevelUps[currentLevel] && !isTimeUp)
            {
                currentLevel++;
                isTimeUp = true;
                
                if(Random.value < currentLevel / 10f)
                    grid.GenerateFood();
                grid.GenerateObstacle();

            }
            else
            {
                isTimeUp = false;
            }

            p1Snake.GetInput();
            p1Snake.CheckMove();

            p2Snake.GetInput();
            p2Snake.CheckMove();
        }
    }

    public void EndGame(EndGameType endGameType)
    {
        if (currentState == GameState.EndGame)
            return;

        Debug.Log("Game ended!");
        currentState = GameState.EndGame;

        switch (endGameType)
        {
            case EndGameType.P1Win:
                Debug.Log("Player 1 Win!");
                break;
            case EndGameType.P2Win:
                Debug.Log("Player 2 Win!");
                break;
            case EndGameType.Draw:
                Debug.Log("Draw!");
                break;
        }

        //  invoke result dialog
        StartCoroutine(IEShowResultDialog(endGameType));
    }

    private IEnumerator IEShowResultDialog(EndGameType endGameType)
    {
        yield return new WaitForSeconds(1f);
        DIContainer.Resolve<UIManager>()
            .Show<ResultDialog>()
            .UpdateResult(endGameType);
    }
}