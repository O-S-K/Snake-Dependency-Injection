using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultDialog : Dialog
{
    [Inject] private UIManager _uiManager;
    [SerializeField] private  Button _playButton;
    [SerializeField] private Text _updateResultText;

    private void Awake()
    {
        _playButton.onClick.AddListener(OnPlayButtonClicked);
    }
    
    
    public override void Show()
    {
        base.Show();
    }

    public void UpdateResult(GameController.EndGameType type)
    {
        switch (type)
        {
            case GameController.EndGameType.P1Win:
                _updateResultText.text = "--- Player 1 Win! --- ";
                break;
            case GameController.EndGameType.P2Win:
                _updateResultText.text = " --- Player 2 Win! --- ";
                break;
            case GameController.EndGameType.Draw:
                _updateResultText.text = " --- Draw! --- ";
                break;
        }
    } 
        
    private void OnPlayButtonClicked()
    {
        SceneManager.LoadScene(0);
    }

}