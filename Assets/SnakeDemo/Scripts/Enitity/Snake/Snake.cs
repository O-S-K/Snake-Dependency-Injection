using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour, ISnake
{
    private List<Transform> tail;
    private Vector2 direction;
    private Vector2 nextDirection;

    private float moveTimer;
    public GameObject tailPrefab; // Prefab của đoạn đuôi mới
    private int currentLevel = 0;

    [Inject] private Camera mainCamera;
    [Inject] private IDataSO dataSo;
    [Inject] private IGameController gameController;
    [Inject] private IGrid grid;
    [Inject] private IInput input;


    public void Init()
    {
        tail = new List<Transform> { transform };
        tail[0].position = Vector2.zero;
        nextDirection = Vector2.zero;
        moveTimer = 0;
    }

    public void GetInput()
    {
        Vector2 newInput = input.GetInput();
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
            Vector2 newPosition = tail[0].position + (Vector3)direction;

            // Kiểm tra nếu rắn ra khỏi màn hình và đặt lại vị trí
            newPosition = CheckOutOfBounds(newPosition);

            // Kiểm tra va chạm với đuôi
            if (CheckCollisonTail(newPosition)) return;

            // Di chuyển từng đoạn đuôi
            MoveTails();
            MoveHead(newPosition);
        }
    }

    private void MoveHead(Vector2 newPosition)
    {
        tail[0].position = newPosition;
        moveTimer = dataSo.MoveInterval; 
    }

    private void MoveTails()
    {
        for (int i = tail.Count - 1; i > 0; i--)
        {
            tail[i].position = tail[i - 1].position;
        }
    }

    private bool CheckCollisonTail(Vector2 newPosition)
    {
        for (int i = 1; i < tail.Count; i++)
        {
            if ((Vector2)tail[i].position == newPosition)
            {
                gameController.EndGame();
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
        switch (other.tag)
        {
            case "Food":
            {
                Grow();
                grid.GenerateFood();
                Destroy(other.gameObject);
                
                GameData.AddScore(1);
                
                var ui = DIContainer.Resolve<UIManager>().Get<IngameDialog>();
                ui.UpdateScore(GameData.Score);
                
                if(GameData.Score > 0 && GameData.Score % dataSo.LevelUps[currentLevel] == 0)
                {
                    if(currentLevel < 5)
                        currentLevel++;
                    grid.GenerateObstacle();
                }
                break;
            }
            case "Obstacle":
            {
                nextDirection = Vector2.zero;
                gameController.EndGame();
                break;
            }
        }
    }

    public void Grow()
    {
        var newTail = Instantiate(tailPrefab, tail[^1].position, Quaternion.identity);
        tail.Add(newTail.transform);
    }

    public void CheckCollision()
    {
        // Check collision with walls or self
        // Check collision with food
        // If collision with food, call Grow() and grid.GenerateFood()
    }

    public Vector2[] GetPosition()
    {
        Vector2[] positions = new Vector2[tail.Count];
        for (int i = 0; i < tail.Count; i++)
        {
            positions[i] = tail[i].position;
        }
        return positions;
    }
}