// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UnityEngine.UI;
using UI;

public class DefeatMenu : Window
{
    [SerializeField] private Button _restartButton;

    private void Awake()
    {
        _restartButton.onClick.AddListener(RestartScene);
    }

    private void RestartScene()
    {
        GameLogicController.RestartScene();
    }

    private void OnDestroy()
    {
        _restartButton.onClick.RemoveListener(RestartScene);
    }
}