using UnityEngine;

public interface IGrid
{
    void Init();
    void GenerateFood();
    Vector2 GetSize();
}