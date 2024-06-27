using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    public DataSO dataSO;

    private void Awake()
    {
        // Instantiate các instance
        var ui = Instantiate(Resources.Load<UIManager>("UICanvas"), transform);
        var cam = Instantiate(Resources.Load<Camera>("Main Camera"), transform);
        
        var data = new GameObject("GameData").AddComponent<GameData>();
        data.transform.parent = transform;
        
        var gameController = Instantiate(dataSO.GameController, transform);
        var grid = Instantiate(dataSO.GridPrefab, transform);
        var snake = Instantiate(dataSO.SnakePrefab, transform);
        var input = snake.gameObject.AddComponent<PlayerInput>();

        // Bind DataSO vào container
        DIContainer.Bind<IDataSO>(() => dataSO);
        DIContainer.Bind<GameData>(() => data);
        DIContainer.Bind<UIManager>(() => ui);
        DIContainer.Bind<IGameController>(() => gameController);
        DIContainer.Bind<Camera>(() => cam);
        DIContainer.Bind<ISnake>(() => snake);
        DIContainer.Bind<IGrid>(() => grid);
        DIContainer.Bind<IInput>(() => input);
        

        // Lấy các dependencies từ container
        DIContainer.Inject(dataSO);
        DIContainer.Inject(data);
        DIContainer.Inject(gameController);
        DIContainer.Inject(cam);
        DIContainer.Inject(ui);
        DIContainer.Inject(snake);
        DIContainer.Inject(grid);
        DIContainer.Inject(input);
        
        // Khởi tạo game 
        gameController.Init();
        ui.Show<MenuDialog>();
    }
}