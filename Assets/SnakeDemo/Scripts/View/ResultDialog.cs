using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultDialog : Dialog
{
    [Inject] private UIManager _uiManager;
    [SerializeField] private  Button _playButton;
    [SerializeField] private Text _scoreText;

    private void Awake()
    {
        _playButton.onClick.AddListener(OnPlayButtonClicked);
    }
    
    public void UpdateScore(int score)
    {
        _scoreText.text = "Score: " + score;
    }
    
    private void OnPlayButtonClicked()
    {
        SceneManager.LoadScene(0);
    }

    public override void Show()
    {
        UpdateScore(GameData.Score);
        base.Show();
    } 
}