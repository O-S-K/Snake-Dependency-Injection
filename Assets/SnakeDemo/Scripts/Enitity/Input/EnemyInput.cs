using UnityEngine;

public class EnemyInput : MonoBehaviour, IInput
{
    public Vector2 GetInput(float moveStep)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            return Vector2.up * moveStep;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            return Vector2.down * moveStep;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            return Vector2.left * moveStep;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            return Vector2.right * moveStep;
        }

        return Vector2.zero;
    }

}