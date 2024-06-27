using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour, IGrid
{
    public GameObject gridCellPrefab;

    private List<Vector2Int> occupiedPositions = new List<Vector2Int>(); 
    private GameObject[,] gridCells;
    private Vector2Int gridSize;
 
    [Inject] private IDataSO dataSO;
    
    public void Init()
    { 
        gridSize = dataSO.GridSize;
        CreateCells(); 
        GenerateFood();
        GenerateObstacles();
    }

    private void CreateCells( )
    {
        gridCells = new GameObject[gridSize.x, gridSize.y];

        // Tạo lưới và đánh dấu các ô đã chiếm
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
        // Tạo danh sách ngẫu nhiên vị trí
        List<Vector2Int> randomPositions = new List<Vector2Int>(occupiedPositions);
        Shuffle(randomPositions);

        // Lấy vị trí cho thức ăn (chỉ lấy một vị trí ngẫu nhiên)
        if (randomPositions.Count > 0)
        {
            Vector2Int foodPosition = randomPositions[0];
            Instantiate(dataSO.FoodPrefab,GridToWorldPosition(foodPosition), Quaternion.identity, transform);
            randomPositions.RemoveAt(0);
        }
    }

    private void GenerateObstacles()
    {
        // Lấy vị trí cho chướng ngại vật (sử dụng tất cả các vị trí còn lại)
        foreach (Vector2Int position in occupiedPositions)
        {
            // Kiểm tra nếu vị trí này không phải vị trí của thức ăn
            if (!IsFoodPosition(position))
            {
                 Instantiate(dataSO.ObstaclePrefab, GridToWorldPosition(position), Quaternion.identity, transform);
                 break;
            }
        }
    }

    private bool IsFoodPosition(Vector2Int position)
    {
        // Kiểm tra xem vị trí có phải là vị trí của thức ăn không
        GameObject foodObject = GameObject.FindGameObjectWithTag("Food");
        if (foodObject != null)
        {
            Vector2 foodWorldPosition = foodObject.transform.position;
            Vector2 foodGridPosition =
                new Vector2(Mathf.RoundToInt(foodWorldPosition.x), Mathf.RoundToInt(foodWorldPosition.y));
            Vector2Int foodGridPositionInt = new Vector2Int((int)foodGridPosition.x, (int)foodGridPosition.y);
            return position == foodGridPositionInt;
        }
        return false;
    }

    private Vector2 GridToWorldPosition(Vector2Int gridPosition)
    {
        float x = gridPosition.x - gridSize.x / 2;
        float y = gridPosition.y - gridSize.y / 2;
        return new Vector2(x, y);
    }

    // Đảo thứ tự các phần tử trong danh sách ngẫu nhiên
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