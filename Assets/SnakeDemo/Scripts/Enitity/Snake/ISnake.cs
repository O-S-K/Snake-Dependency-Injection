using UnityEngine;

public interface ISnake
{
    void Init();
    void GetInput();
    void CheckMove();
    void Grow();
    void CheckCollision();
    Vector2[] GetPosition();
}