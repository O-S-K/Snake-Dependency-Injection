using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDialog : Dialog
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _exitButton;

    private void Awake()
    {
        _playButton.onClick.AddListener(OnPlayButtonClicked);
        _exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnPlayButtonClicked()
    {
        DIContainer.Resolve<IGameController>().Init();
        DIContainer.Resolve<UIManager>().Show<IngameDialog>();
    }
    
    private void OnExitButtonClicked()
    {
        Application.Quit();
    }

    public override void Show()
    {
        base.Show();

        if (GameInstaller.IsLoadedIngame)
        {
            DIContainer.Resolve<IGameController>().Init();
            DIContainer.Resolve<UIManager>().Show<IngameDialog>();
        }
    }
}