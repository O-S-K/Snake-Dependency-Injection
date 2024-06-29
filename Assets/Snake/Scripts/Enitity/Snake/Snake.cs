using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Snake : MonoBehaviour, ISnake
{
    protected Vector2 direction;
    protected Vector2 nextDirection;

    protected float moveTimer;
    protected Color color;

    // Injected
    [Inject] protected DataSO dataSo;
    [Inject] protected IGameController gameController;
    [Inject] protected IGrid grid;

    protected ITail tail;
    protected IInput input;
    
    protected AudioSource audioSource; 
    public virtual void Init(Color color)
    {
        this.color = color;
        GetComponent<SpriteRenderer>().color = color;
        
        audioSource = gameObject.AddComponent<AudioSource>();

        var _tail = gameObject.AddComponent<TailComponent>();
        _tail.Init(dataSo.TailPrefab);
        tail = _tail;

        nextDirection = Vector2.zero;
        moveTimer = 0;
    }

    public virtual void GetInput()
    {
        Vector2 newInput = input.GetInput(dataSo.MoveStep);
        if (newInput != Vector2.zero && newInput != -direction)
        {
            nextDirection = newInput;
        }
    }

    public virtual void CheckMove()
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

    protected virtual void MoveHead(Vector2 newPosition)
    {
        tail.GetTail(0).position = newPosition;
        moveTimer = dataSo.MoveInterval;
    }

    protected bool CheckCollisionTail(Vector2 newPosition)
    {
        for (int i = 1; i < tail.GetTailCount(); i++)
        {
            if ((Vector2)tail.GetTail(i).position == newPosition)
            {
                CollisionTail();
                return true;
            }
        }

        return false;
    }

    protected virtual void CollisionTail()
    {
    }

    protected virtual Vector2 CheckOutOfBounds(Vector2 position)
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

    
    protected virtual void TriggerFood(Collider2D other)
    {
        transform.DOShakeScale(0.1f, 0.1f, 10, 90, false);
        audioSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/EatFood"), 2);
        Destroy(other.gameObject);
        Grow();
        grid.GenerateFood();
    }
    
    protected virtual void TriggerObstacle(GameController.EndGameType endGameType)
    {
        audioSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/Hit"), 2);
        Camera.main.transform.DOShakePosition(0.35f, 0.5f, 10, 90, false);
        transform.DOShakeScale(0.1f, 0.1f, 10, 90, false);
        nextDirection = Vector2.zero;
        gameController.EndGame(endGameType);
    }

    public virtual void Grow()
    {
    }
    public Vector2[] GetPosition() => tail.GetPosition();
    public Color GetColor() => color;
}