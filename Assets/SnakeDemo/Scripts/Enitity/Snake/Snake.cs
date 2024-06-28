using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private List<Transform> tail;
    private Vector2 direction;
    private Vector2 nextDirection;

    private float moveTimer;
    public GameObject tailPrefab; // Prefab của đoạn đuôi mới
    private int currentLevel = 0;
}
