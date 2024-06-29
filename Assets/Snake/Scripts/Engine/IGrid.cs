using UnityEngine;

public interface IGrid
{
    void Init();
    void GenerateFood();
    void GenerateObstacle();
    Vector2 GetSize();
    
    Vector2 GetPositionEmptyCell();
}