using UnityEngine;

public class P1Snake : Snake
{
    public override void Init(Color color)
    {
        base.Init(color);
        tag = Tags.Player1; 
        name = "Player 1";

        tail.GetTail(0).position = new Vector2(-1, 0);
        var _input = gameObject.AddComponent<PlayerInput>();
        input = _input; 
    }
   
    protected override void CollisionTail()
    {
        gameController.EndGame(GameController.EndGameType.P2Win);
    }

    protected override void TriggerFood(Collider2D other)
    {
        base.TriggerFood(other);
        
        gameController.AddScore(0);

        var ui = DIContainer.Resolve<UIManager>().Get<IngameDialog>();
        ui.UpdateScore(1, GameData.ScorePlayer);
    }
    
    protected override void TriggerObstacle(Collision2D collision)
    {
        base.TriggerObstacle(collision);
        gameController.EndGame(GameController.EndGameType.P2Win);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!gameController.IsInGame())
            return;
        if (collision.gameObject.CompareTag(Tags.Obstacle)) 
        { 
            TriggerObstacle(collision);
        }
        else if (collision.gameObject.TryGetComponent(out P2Snake p2Snake))
        {
            if (tail.GetTailCount() > p2Snake.GetPosition().Length)
            {
                gameController.EndGame(GameController.EndGameType.P1Win);
                nextDirection = Vector2.zero;
            }
            else if (tail.GetTailCount() < p2Snake.GetPosition().Length)
            {
                gameController.EndGame(GameController.EndGameType.P2Win);
                nextDirection = Vector2.zero;
            }
            else
            {
                gameController.EndGame(GameController.EndGameType.Draw);
                nextDirection = Vector2.zero;
            }
        }
    }

    public override void Grow()
    {
        base.Grow();
        tail.AddTail(Tags.Player1, color);
    }
}