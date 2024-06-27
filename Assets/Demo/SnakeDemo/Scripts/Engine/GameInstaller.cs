using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    public DataSO dataSO;

    private void Start()
    {
        // Instantiate các instance
        var gameController = Instantiate(dataSO.GameController);
        var snake = Instantiate(dataSO.SnakePrefab);
        var input = snake.gameObject.AddComponent<PlayerInput>();
        var grid = Instantiate(dataSO.GridPrefab);

        // Bind DataSO vào container
        DIContainer.BindAndProvide<IDataSO>(() => dataSO);
        DIContainer.BindAndProvide<IGameController>(() => gameController);
        DIContainer.BindAndProvide<ISnake>(() => snake);
        DIContainer.BindAndProvide<IGrid>(() => grid);
        DIContainer.BindAndProvide<IInput>(() => input);

        // Lấy các dependencies từ container
        DIContainer.Inject(dataSO);
        DIContainer.Inject(gameController);
        DIContainer.Inject(snake);
        DIContainer.Inject(grid);
        DIContainer.Inject(input);

        // Khởi tạo game
        gameController.Init();
    }
}