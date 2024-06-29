using UnityEngine;

public interface ITail
{
    void Init(GameObject tailPrefab);
    Transform GetTail(int index);
    int GetTailCount();
    void AddTail(string tag, Color color);
    void MoveTails();
    Vector2[] GetPosition();
}