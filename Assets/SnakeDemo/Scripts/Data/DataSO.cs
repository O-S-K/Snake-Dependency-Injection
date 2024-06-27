using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataSO
{
    GameController GameController { get; }
    Snake SnakePrefab { get; }
    Grid GridPrefab { get; }
    Food FoodPrefab { get; }
    Obstacle ObstaclePrefab { get; }
    Vector2Int GridSize { get; }
    float MoveInterval { get; }
    float MoveStep { get; }
    int[] LevelUps { get; }
}


[CreateAssetMenu(fileName = "EnitiesDataSO", menuName = "ScriptableObjects/EnitiesDataSO", order = 1)]
public class DataSO : ScriptableObject, IDataSO
{
    [SerializeField] private GameController gameController;
    [SerializeField] private Snake snakePrefab;
    [SerializeField] private Grid gridPrefab;
    [SerializeField] private Food foodPrefab;
    [SerializeField] private Obstacle obstaclePrefab;
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private float moveStep;
    [SerializeField] private float moveInterval;

    [SerializeField] private int[] levelUps;


    public GameController GameController => gameController;

    public Snake SnakePrefab => snakePrefab;

    public Grid GridPrefab => gridPrefab;

    public Food FoodPrefab => foodPrefab;

    public Obstacle ObstaclePrefab => obstaclePrefab;

    public Vector2Int GridSize => gridSize;

    public float MoveInterval => moveInterval;

    public float MoveStep => moveStep;

    public int[] LevelUps => levelUps;
}