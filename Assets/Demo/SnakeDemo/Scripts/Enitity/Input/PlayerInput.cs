using UnityEngine;

public class PlayerInput : MonoBehaviour, IInput
{
    [Inject] private IDataSO dataSO;

    public Vector2 GetInput()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            return Vector2.up * dataSO.MoveStep;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            return Vector2.down * dataSO.MoveStep;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            return Vector2.left * dataSO.MoveStep;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            return Vector2.right * dataSO.MoveStep;
        }

        return Vector2.zero;
    }

}