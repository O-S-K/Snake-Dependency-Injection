using UnityEngine;

public class Obstacle : MonoBehaviour, IObstacle
{
    public void Init(Color colorGraphic, string tag)
    {
        GetComponent<SpriteRenderer>().color = colorGraphic;
        gameObject.tag = tag;
    }
}