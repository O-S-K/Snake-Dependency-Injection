using TMPro;
using UnityEngine;

public class P2Snake : Snake
{
    public override void Init(Color color)
    {
        base.Init(color);
        tag = Tags.Player2;
        name = "Player 2";
        
        var nameText =transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        nameText.text = "P2";
        
        tail.GetTail(0).position = grid.GetPositionEmptyCell();
        var _input = gameObject.GetOrAdd<EnemyInput>();
        input = _input; 
    }
     
    protected override void CollisionTail()
    {
        Interactive(GameController.EndGameType.P1Win);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == Tags.Food)
        {
            TriggerFood(other);
        }
        else if(other.tag == Tags.Player1)
        {
            nextDirection = Vector2.zero;
            gameController.EndGame(GameController.EndGameType.P1Win);
        } 
    }
    

    protected override void TriggerFood(Collider2D other)
    {
        base.TriggerFood(other);
        
        gameController.AddScore(1);

        var ui = DIContainer.Resolve<UIManager>().Get<IngameDialog>();
        ui.UpdateScore(2, GameData.ScoreEnemy);
    }  

    protected override void Interactive(GameController.EndGameType endGameType)
    {
        base.Interactive(endGameType); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!gameController.IsInGame())
            return;

        if (collision.gameObject.CompareTag(Tags.Obstacle)) 
        { 
            Interactive(GameController.EndGameType.P1Win);
        }
        // trigger tail of player 1
        else if(collision.collider.CompareTag(Tags.Player1) && collision.collider.isTrigger)
        {
            Interactive(GameController.EndGameType.P1Win);
        } 
        else if (collision.gameObject.TryGetComponent(out P1Snake p1Snake))
        { 
            if (tail.GetTailCount() > p1Snake.GetPosition().Length)
            {
                Interactive(GameController.EndGameType.P2Win);
            }
            else if (tail.GetTailCount() < p1Snake.GetPosition().Length)
            {
                Interactive(GameController.EndGameType.P1Win);
            }
            else
            {
                Interactive(GameController.EndGameType.Draw);
            }
        }
    }

    public override void Grow()
    {
        base.Grow();
        tail.AddTail(Tags.Player2, color);
    } 
}