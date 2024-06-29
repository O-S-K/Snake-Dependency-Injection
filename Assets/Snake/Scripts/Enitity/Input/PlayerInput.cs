using UnityEngine;

public class PlayerInput : MonoBehaviour, IInput
{
    public Vector2 GetInput(float moveStep)
    {
        if (Input.GetKeyDown(KeyCode.W) )
        {
            return Vector2.up * moveStep;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            return Vector2.down * moveStep;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            return Vector2.left * moveStep;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            return Vector2.right * moveStep;
        }
        return Vector2.zero;
    }
}