using System;
using TMPro;
using UnityEngine;

public class P1Snake : Snake
{
    public override void Init(Color color)
    {
        base.Init(color);
        tag = Tags.Player1; 
        name = "Player 1";

        var nameText =transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        nameText.text = "P1";

        tail.GetTail(0).position = grid.GetPositionEmptyCell();
        var _input = gameObject.GetOrAdd<PlayerInput>();
        input = _input; 
    }
   
    protected override void CollisionTail()
    {
        gameController.EndGame(GameController.EndGameType.P2Win);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == Tags.Food)
        {
            TriggerFood(other);
        }
        else if(other.tag == Tags.Player2)
        {
            TriggerObstacle(GameController.EndGameType.P2Win);
        } 
    }

    protected override void TriggerFood(Collider2D other)
    {
        base.TriggerFood(other);
        
        gameController.AddScore(0);

        var ui = DIContainer.Resolve<UIManager>().Get<IngameDialog>();
        ui.UpdateScore(1, GameData.ScorePlayer);
    }
    
    protected override void TriggerObstacle(GameController.EndGameType endGameType)
    {
        base.TriggerObstacle(endGameType); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!gameController.IsInGame())
            return;
        if (collision.gameObject.CompareTag(Tags.Obstacle)) 
        { 
            TriggerObstacle(GameController.EndGameType.P2Win);
        } 
        // trigger tail of player 2
        else if(collision.collider.CompareTag(Tags.Player2))
        {
            TriggerObstacle(GameController.EndGameType.P2Win);
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
                TriggerObstacle(GameController.EndGameType.P2Win);
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