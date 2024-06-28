using UnityEngine;

public class P2Snake : Snake
{
    public override void Init(Color color)
    {
        base.Init(color);
        tag = Tags.Player2;
        
        tail.GetTail(0).position = new Vector2(1, 0);
        var _input = gameObject.AddComponent<EnemyInput>();
        input = _input; 
    }
     
    protected override void CollisionTail()
    {
        gameController.EndGame(GameController.EndGameType.P1Win);
    }

    protected override void TriggerFood(Collider2D other)
    {
        base.TriggerFood(other);
        
        gameController.AddScore(1);

        var ui = DIContainer.Resolve<UIManager>().Get<IngameDialog>();
        ui.UpdateScore(2, GameData.ScoreEnemy);

        if (GameData.ScoreEnemy > 0 && GameData.ScoreEnemy % dataSo.LevelUps[currentLevel] == 0)
        {
            if (currentLevel < 4)
                currentLevel++;
            grid.GenerateObstacle();
        }
    }  

    protected override void TriggerObstacle(Collision2D collision)
    {
        base.TriggerObstacle(collision);
        gameController.EndGame(GameController.EndGameType.P1Win);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!gameController.IsInGame())
            return;

        if (collision.gameObject.CompareTag(Tags.Obstacle)) 
        { 
            TriggerObstacle(collision);
        }
        else if (collision.gameObject.TryGetComponent(out P1Snake p1Snake))
        {
            if (tail.GetTailCount() > p1Snake.GetPosition().Length)
            {
                gameController.EndGame(GameController.EndGameType.P2Win);
                nextDirection = Vector2.zero;
            }
            else if (tail.GetTailCount() < p1Snake.GetPosition().Length)
            {
                gameController.EndGame(GameController.EndGameType.P1Win);
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
        tail.AddTail(Tags.Player2, color);
    } 
}