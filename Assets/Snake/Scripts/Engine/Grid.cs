using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Grid : MonoBehaviour, IGrid
{
    public GameObject gridCellPrefab;

    private List<Vector2Int> occupiedPositions = new List<Vector2Int>();
    private GameObject[,] gridCells;
    private Vector2Int gridSize;

    [Inject] private DataSO dataSO;
    [Inject] private P1Snake p1Snake;
    [Inject] private P2Snake p2Snake;

    private Vector2 food;
    private Vector2 obstacle;

    public void Init()
    {
        gridSize = dataSO.GridSize;
        CreateCells();
        
        StartCoroutine(AnimateCells( () =>
        { 
            p1Snake.transform.DOScale(1, 0.5f);
            p2Snake.transform.DOScale(1, 0.5f);
            GenerateObstacle();
            DIContainer.Resolve<IGameController>().StartGame();
        }));
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
                cell.transform.localScale = Vector3.zero; // Bắt đầu với scale 0
            }
        }
    }

    private IEnumerator AnimateCells(System.Action callback = null)
    {
        float delay = 0.001f; 
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                AnimateCell(gridCells[x, y]);
                yield return new WaitForSeconds(delay);
            }
        }
        callback?.Invoke();
    }

    private void AnimateCell(GameObject cell)
    {
        cell.transform.DOScale(Vector3.one, 0.1f).SetEase(Ease.Linear);
    }

    public void GenerateFood()
    {
        var f = Instantiate(dataSO.EnityGraphicsPrefab, RandomPositionFood(), Quaternion.identity, transform);
        f.transform.DOShakeScale(1, 0.5f, 10, 90);
        food = f.transform.position.normalized;
    }

    public void GenerateObstacle()
    {
        var o = Instantiate(dataSO.ObstaclePrefab, RandomPositionObstacle(), Quaternion.identity, transform);
        o.transform.DOScale(0, 0).OnComplete(() => { o.transform.DOScale(1, 0.2f); });
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
                !IsPositionSnake(p2Snake.GetPosition(), position))
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
                !IsPositionSnake(p2Snake.GetPosition(), position))
            {
                return GridToWorldPosition(position);
            }
        }

        return Vector2.zero;
    }


    private bool IsFoodPosition(Vector2Int position)
    {
        return Vector2.Distance(food, GridToWorldPosition(position)) <= 0.01f;
    }

    private bool IsObstaclePosition(Vector2Int position)
    {
        return Vector2.Distance(obstacle, GridToWorldPosition(position)) <= 0.01f;
    }

    private bool IsPositionSnake(Vector2[] positions, Vector2Int position)
    {
        for (int i = 0; i < positions.Length; i++)
        {
            if (Vector2.Distance(positions[i], GridToWorldPosition(position)) <= 0.01f)
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

    public Vector2 GetPositionEmptyCell()
    {
        List<Vector2Int> emptyPositions = new List<Vector2Int>(occupiedPositions);
        Shuffle(emptyPositions);

        foreach (Vector2Int position in emptyPositions)
        {
            if (!IsFoodPosition(position) && !IsObstaclePosition(position))
            {
                return GridToWorldPosition(position);
            }
        }

        return Vector2.zero;
    }
}