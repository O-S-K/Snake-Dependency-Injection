using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "EnitiesDataSO", menuName = "ScriptableObjects/EnitiesDataSO", order = 1)]
public class DataSO : ScriptableObject
{
    [SerializeField] private GameController gameController;
    [SerializeField] private Grid gridPrefab;
    [SerializeField] private Vector2Int gridSize;

    [SerializeField] private GameObject snakePrefab;
    [SerializeField] private GameObject tailPrefab;
    [SerializeField] private Color[] snakeColors;

    
    [SerializeField] private float moveStep;
    [SerializeField] private float moveInterval;
    
    [FormerlySerializedAs("objectSpawnPrefab")] [FormerlySerializedAs("foodPrefab")] [SerializeField] private EnityGraphics enityGraphicsPrefab;
    [SerializeField] private GameObject obstaclePrefab;

    [SerializeField] private int[] levelUps;


    public GameController GameController => gameController;
    public GameObject SnakePrefab => snakePrefab;
    public GameObject TailPrefab => tailPrefab;
    public Grid GridPrefab => gridPrefab;
    public EnityGraphics EnityGraphicsPrefab => enityGraphicsPrefab;
    public GameObject ObstaclePrefab => obstaclePrefab;
    public Vector2Int GridSize => gridSize;

    
    public Color[] SnakeColors => snakeColors;
    public float MoveInterval => moveInterval;
    public float MoveStep => moveStep;
    public int[] LevelUps => levelUps;
}