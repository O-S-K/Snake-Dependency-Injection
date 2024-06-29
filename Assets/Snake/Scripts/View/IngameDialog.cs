using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class IngameDialog : Dialog
{
    [SerializeField] private Text _scorePlayer1Text;
    [SerializeField] private Text _scorePlayer2Text;
 
    public override void Show()
    {
        var p1Snake = DIContainer.Resolve<P1Snake>();
        var p2Snake = DIContainer.Resolve<P2Snake>();
        
        Init(p1Snake.GetColor(), p2Snake.GetColor());
        UpdateScore(1, 0);
        UpdateScore(2, 0);
        
        base.Show();
    }
    
    public void Init(Color playerColor, Color enemyColor)
    {
        _scorePlayer1Text.color = playerColor;
        _scorePlayer2Text.color = enemyColor;
    }

    public void UpdateScore(int playerType, int score)
    {
        if (playerType == 1)
        {
            _scorePlayer1Text.text = "P1: " + score;
        }
        else
        {
            _scorePlayer2Text.text = "P2: " + score;
        } 

    }
 

    public override void Hide()
    {
        base.Hide();
    }
}