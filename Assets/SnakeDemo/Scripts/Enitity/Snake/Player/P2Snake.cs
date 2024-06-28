using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Snake : MonoBehaviour, ISnake
{
    private Vector2 direction;
    private Vector2 nextDirection;

    private float moveTimer;
    private int currentLevel = 0;
    private Color color;

    // Injected
    [Inject] private DataSO dataSo;
    [Inject] private IGameController gameController;
    [Inject] private IGrid grid;

    private ITail tail;
    private IInput input;

    public void Init(Color color)
    {
        name = "Player 2";
        tag = Tags.Player2;
        this.color = color;
        GetComponent<SpriteRenderer>().color = color;
        var _tail = gameObject.AddComponent<TailComponent>();
        _tail.Init(dataSo.TailPrefab);
        tail = _tail;
        tail.GetTail(0).position = new Vector2(1, 0);

        var _input = gameObject.AddComponent<EnemyInput>();
        input = _input;

        nextDirection = Vector2.zero;
        moveTimer = 0;
    }

    public void GetInput()
    {
        Vector2 newInput = input.GetInput(dataSo.MoveStep);
        if (newInput != Vector2.zero && newInput != -direction)
        {
            nextDirection = newInput;
        }
    }

    public void CheckMove()
    {
        // Kiểm tra nếu đã tới thời gian di chuyển tiếp theo
        moveTimer -= Time.deltaTime;
        if (moveTimer <= 0)
        {
            direction = nextDirection;
            Vector2 newPosition = tail.GetTail(0).position + (Vector3)direction;

            // Kiểm tra nếu rắn ra khỏi màn hình và đặt lại vị trí
            newPosition = CheckOutOfBounds(newPosition);

            // Kiểm tra va chạm với đuôi
            if (CheckCollisionTail(newPosition))
                return;

            // Di chuyển từng đoạn đuôi
            tail.MoveTails();
            MoveHead(newPosition);
        }
    }

    private void MoveHead(Vector2 newPosition)
    {
        tail.GetTail(0).position = newPosition;
        moveTimer = dataSo.MoveInterval;
    }

    private bool CheckCollisionTail(Vector2 newPosition)
    {
        for (int i = 1; i < tail.GetTailCount(); i++)
        {
            if ((Vector2)tail.GetTail(i).position == newPosition)
            {
                gameController.EndGame(GameController.EndGameType.P1Win);
                return true;
            }
        }

        return false;
    }

    private Vector2 CheckOutOfBounds(Vector2 position)
    {
        if (position.x >= grid.GetSize().x / 2) // Nếu ra khỏi cạnh phải
        {
            position.x = -grid.GetSize().x / 2;
        }
        else if (position.x < -grid.GetSize().x / 2) // Nếu ra khỏi cạnh trái
        {
            position.x = grid.GetSize().x / 2 - dataSo.MoveStep;
        }
        else if (position.y >= grid.GetSize().y / 2) // Nếu ra khỏi cạnh trên
        {
            position.y = -grid.GetSize().y / 2;
        }
        else if (position.y < -grid.GetSize().y / 2) // Nếu ra khỏi cạnh dưới
        {
            position.y = grid.GetSize().y / 2 - dataSo.MoveStep;
        }

        return position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == Tags.Food)
        {
            Grow();
            grid.GenerateFood();
            Destroy(other.gameObject);

            gameController.AddScore(1);

            var ui = DIContainer.Resolve<UIManager>().Get<IngameDialog>();
            ui.UpdateScore(2, GameData.ScoreEnemy);

            if (GameData.ScoreEnemy > 0 && GameData.ScoreEnemy % dataSo.LevelUps[currentLevel] == 0)
            {
                if (currentLevel < 4)
                    currentLevel++;
                grid.GenerateObstacle();
            }
        }  
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!gameController.IsInGame())
            return;

        if (collision.gameObject.CompareTag(Tags.Obstacle)) 
        { 
            gameController.EndGame(GameController.EndGameType.P1Win);
            nextDirection = Vector2.zero;
        }
        else if (collision.gameObject.TryGetComponent(out P1Snake p1Snake))
        {
            if (tail.GetTailCount() > p1Snake.GetPosition().Length)
            {
                gameController.EndGame(GameController.EndGameType.P2Win);
                nextDirection = Vector2.zero;
            }
            else if (tail.GetTailCount() < p1Snake.GetPosition().Length)
            {
                gameController.EndGame(GameController.EndGameType.P1Win);
                nextDirection = Vector2.zero;
            }
            else
            {
                gameController.EndGame(GameController.EndGameType.Draw);
                nextDirection = Vector2.zero;
            }
        }
    }

    public void Grow() => tail.AddTail(Tags.Player2, color);
    public Vector2[] GetPosition() => tail.GetPosition();
    public Color GetColor() => color;
}