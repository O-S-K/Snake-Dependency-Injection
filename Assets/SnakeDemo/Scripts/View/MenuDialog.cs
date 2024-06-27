using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDialog : Dialog
{
    //[Inject] private UIManager _uiManager;
    [SerializeField] private Button _playButton;

    private void Awake()
    {
        _playButton.onClick.AddListener(OnPlayButtonClicked);
    }

    private void OnPlayButtonClicked()
    {
        DIContainer.Resolve<IGameController>().StartGame();
        DIContainer.Resolve<UIManager>().Show<IngameDialog>();
    }

    public override void Show()
    {
        base.Show();
    }
}