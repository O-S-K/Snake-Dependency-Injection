using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour, IGrid
{
    public GameObject gridCellPrefab;

    private List<Vector2Int> occupiedPositions = new List<Vector2Int>();
    private GameObject[,] gridCells;
    private Vector2Int gridSize;

    [Inject] private DataSO dataSO;
    [Inject] private P1Snake p1Snake;
    [Inject] private P1Snake eSnake;

    private Vector2 food;
    private Vector2 obstacle;

    public void Init()
    {
        gridSize = dataSO.GridSize;
        CreateCells();
        GenerateFood();
        GenerateObstacle();
    }

    private void CreateCells()
    {
        gridCells = new GameObject[gridSize.x, gridSize.y];
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2 position = new Vector2(x - gridSize.x / 2, y - gridSize.y / 2);
                var cell = Instantiate(gridCellPrefab, position, Quaternion.identity);
                cell.transform.parent = transform;
                gridCells[x, y] = cell;
                occupiedPositions.Add(new Vector2Int(x, y));
            }
        }
    }

    public void GenerateFood()
    {
        var f = Instantiate(dataSO.FoodPrefab, RandomPositionFood(), Quaternion.identity, transform);
        food = f.transform.position.normalized;
    }

    public void GenerateObstacle()
    {
        var o = Instantiate(dataSO.ObstaclePrefab, RandomPositionObstacle(), Quaternion.identity, transform);
        obstacle = o.transform.position.normalized;
    }

    private Vector2 RandomPositionFood()
    {
        List<Vector2Int> randomPositions = new List<Vector2Int>(occupiedPositions);
        Shuffle(randomPositions);

        foreach (Vector2Int position in randomPositions)
        {
            if (!IsFoodPosition(position) && !IsObstaclePosition(position) &&
                !IsPositionSnake(p1Snake.GetPosition(), position) &&
                !IsPositionSnake(eSnake.GetPosition(), position))
            {
                return GridToWorldPosition(position);
            }
        }

        return Vector2.zero;
    }

    private Vector2 RandomPositionObstacle()
    {
        List<Vector2Int> randomPositions = new List<Vector2Int>(occupiedPositions);
        Shuffle(randomPositions);

        foreach (Vector2Int position in randomPositions)
        {
            if (!IsFoodPosition(position) &&
                !IsObstaclePosition(position) &&
                !IsPositionSnake(p1Snake.GetPosition(), position) &&
                !IsPositionSnake(eSnake.GetPosition(), position))
            {
                return GridToWorldPosition(position);
            }
        }

        return Vector2.zero;
    }


    private bool IsFoodPosition(Vector2Int position)
    {
        return Vector2.Distance(food , GridToWorldPosition(position)) <= 0.01f;
    }

    private bool IsObstaclePosition(Vector2Int position)
    {
        return Vector2.Distance(obstacle , GridToWorldPosition(position)) <= 0.01f;
    }

    private bool IsPositionSnake(Vector2[] positions, Vector2Int position)
    {
        for (int i = 0; i < positions.Length; i++)
        {
            if (Vector2.Distance(positions[i] , GridToWorldPosition(position)) <= 0.01f)
            {
                return true;
            }
        }

        return false;
    }


    private Vector2 GridToWorldPosition(Vector2Int gridPosition)
    {
        float x = gridPosition.x - gridSize.x / 2;
        float y = gridPosition.y - gridSize.y / 2;
        return new Vector2(x, y);
    }

    private void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            (list[randomIndex], list[i]) = (list[i], list[randomIndex]);
        }
    }

    public Vector2 GetSize()
    {
        return gridSize;
    }
}