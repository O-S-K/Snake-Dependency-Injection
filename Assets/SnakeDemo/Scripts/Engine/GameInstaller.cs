using UnityEngine;

public class GameInstaller : MonoBehaviour
{

    private void Awake()
    {
        // Load ScriptableObjects and instantiate necessary objects
        var dataSO = Resources.Load<DataSO>("Data/DataSO");
        var uiPrefab = Resources.Load<UIManager>("UICanvas");
        var camPrefab = Resources.Load<Camera>("Main Camera");
        var gameControllerPrefab = dataSO.GameController;
        var gridPrefab = dataSO.GridPrefab;
        var snakePrefab = dataSO.SnakePrefab;

        // Instantiate objects in the scene
        var ui = Instantiate(uiPrefab, transform);
        var cam = Instantiate(camPrefab, transform);
        var game = Instantiate(gameControllerPrefab, transform);
        var grid = Instantiate(gridPrefab, transform);
        var snake = Instantiate(snakePrefab, transform);
        var input = snake.gameObject.AddComponent<PlayerInput>();

        // Bind dependencies to DIContainer
        DIContainer.Bind<IDataSO>(() => dataSO);
        DIContainer.Bind<UIManager>(() => ui);
        DIContainer.Bind<IGameController>(() => game);
        DIContainer.Bind<Camera>(() => cam);
        DIContainer.Bind<IGrid>(() => grid);
        DIContainer.Bind<ISnake>(() => snake);
        DIContainer.Bind<IInput>(() => input);

        // Inject dependencies into instantiated objects
        DIContainer.Inject(dataSO);
        DIContainer.Inject(game);
        DIContainer.Inject(cam);
        DIContainer.Inject(ui);
        DIContainer.Inject(snake);
        DIContainer.Inject(grid);
        DIContainer.Inject(input);

        // Initialize game components
        game.Init();
        ui.Show<MenuDialog>();
    }
}