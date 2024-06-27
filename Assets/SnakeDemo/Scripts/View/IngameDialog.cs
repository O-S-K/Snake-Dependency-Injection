using UnityEngine;
using UnityEngine.UI;

public class IngameDialog : Dialog
{
    [SerializeField] private Text _scoreText;
    
    public override void Show()
    {
        UpdateScore(0);
        base.Show();
    }
    
    public void UpdateScore(int score)
    {
        _scoreText.text = "Score: " + score;
    }
    
    public override void Hide()
    {
        base.Hide();
    }

}