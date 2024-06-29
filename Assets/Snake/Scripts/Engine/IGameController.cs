

public interface IGameController
{
    void Init();
    void StartGame();
    void UpdateGame();
    void EndGame(GameController.EndGameType type);

    bool IsInGame();
    void AddScore(int type);
}