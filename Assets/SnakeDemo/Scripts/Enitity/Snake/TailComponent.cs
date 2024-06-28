using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailComponent : MonoBehaviour, ITail
{
    private List<Transform> tail;
    private GameObject tailPrefab;

    public void Init(GameObject tailPrefab)
    {
        this.tailPrefab = tailPrefab;
        tail = new List<Transform>();
        tail.Add(transform);
    }

    public void MoveTails()
    {
        for (int i = tail.Count - 1; i > 0; i--)
        {
            tail[i].position = tail[i - 1].position;
        }
    }

    public Vector2[] GetPosition()
    {
        Vector2[] positions = new Vector2[tail.Count];
        for (int i = 0; i < tail.Count; i++)
        {
            positions[i] = tail[i].position;
        }

        return positions;
    }

    public Transform GetTail(int index)
    {
        return tail[index];
    }

    public int GetTailCount()
    {
        return tail.Count;
    }

    public void AddTail(string tag, Color color)
    {
        var newTail = Instantiate(tailPrefab, tail[^1].position, Quaternion.identity);
        newTail.tag = tag;
        newTail.GetComponent<SpriteRenderer>().color = color;
        tail.Add(newTail.transform);
    }
}