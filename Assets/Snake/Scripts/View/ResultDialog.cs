using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultDialog : Dialog
{
    [Inject] private UIManager _uiManager;
    [SerializeField] private  Button _playButton;
    [SerializeField] private  Button _backMenuButton;

    [SerializeField] private Text _updateResultText;

    private void Awake()
    {
        _playButton.onClick.AddListener(OnPlayButtonClicked);
        _backMenuButton.onClick.AddListener(ObBackMenu);
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
    
    private void ObBackMenu()
    {
        GameInstaller.IsLoadedIngame = false;
        SceneManager.LoadScene(0);
    }
        
    private void OnPlayButtonClicked()
    {
        GameInstaller.IsLoadedIngame = true;
        SceneManager.LoadScene(0);
    }

}