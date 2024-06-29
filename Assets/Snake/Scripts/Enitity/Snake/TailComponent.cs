using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
        StartCoroutine(ShakeTail());

        // float scaleFactor = Mathf.Pow(0.98765f, tail.Count);
        // newTail.transform.localScale = new Vector3(1 * scaleFactor, 1 * scaleFactor, 1);
        tail.Add(newTail.transform);
    }
    
    private IEnumerator ShakeTail()
    {
        float delay = 0;
        for (int i = 0; i < tail.Count; i++)
        {
            tail[i].transform.localScale = Vector3.one;
            tail[i].DOScale(1.15f, 0.1f).OnComplete(() =>
            {
                tail[i].DOScale(1, 0.1f);
            });
            yield return new WaitForSeconds(delay);
            delay += 0.05f;
        }
        
        for (int i = 0; i < tail.Count; i++)
        {
            tail[i].transform.localScale = Vector3.one;
        }
    }
}