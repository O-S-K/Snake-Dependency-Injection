using Unity.VisualScripting;
using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    public static bool IsLoadedIngame;
    
    private void Awake()
    {
        // Load ScriptableObjects and instantiate necessary objects
        var dataSO = Resources.Load<DataSO>("Data/DataSO");
        var uiPrefab = Resources.Load<UIManager>("UICanvas");
        var camPrefab = Resources.Load<Camera>("Main Camera");

        // Instantiate objects in the scene
        var ui = Instantiate(uiPrefab, transform);
        var cam = Instantiate(camPrefab, transform);
        var game = Instantiate(dataSO.GameController, transform);

        var snakeP = Instantiate(dataSO.SnakePrefab, transform).AddComponent<P1Snake>();
        snakeP.transform.localScale = Vector3.zero;
        
        var snakeE = Instantiate(dataSO.SnakePrefab, transform).AddComponent<P2Snake>();
        snakeE.transform.localScale = Vector3.zero;

        var grid = Instantiate(dataSO.GridPrefab, transform);

        // Bind dependencies to DIContainer
        DIContainer.Bind<DataSO>(() => dataSO);
        DIContainer.Bind<UIManager>(() => ui);
        DIContainer.Bind<IGameController>(() => game);
        DIContainer.Bind<Camera>(() => cam);

        DIContainer.Bind<P1Snake>(() => snakeP);
        DIContainer.Bind<P2Snake>(() => snakeE);
        DIContainer.Bind<IGrid>(() => grid);

        DIContainer.Injects(new object[]
        {
             dataSO, ui, game, cam, snakeP, snakeE, grid
        });
         

        // Initialize game
        ui.Show<MenuDialog>();
    } 
}