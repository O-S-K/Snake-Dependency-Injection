using UnityEngine;

public interface ISnake
{
    void Init(Color color);
    void GetInput();
    void CheckMove();
    void Grow();
    Vector2[] GetPosition();
    Color GetColor();
}