// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UnityEngine.UI;
using UI;

public class MainPage : Page
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _creatorsButton;
    [SerializeField] private Button _quitButton;

    private void Awake()
    {
        _playButton.onClick.AddListener(LoadCharacterSelect);
        _settingsButton.onClick.AddListener(LoadSettings);
        _creatorsButton.onClick.AddListener(LoadCreators);
        _quitButton.onClick.AddListener(LoadQuit);
    }

    private void Start()
    {
        AudioManager.Instance.PlayMusic("Ambient2");
        AudioManager.Instance.LoopMusic();
    }

    private void LoadCharacterSelect()
    {
        PageManager.OpenPage<CharacterSelectPage>();
    }

    private void LoadSettings()
    {
        WindowManager.OpenWindow<SettingsWindow>();
    }

    private void LoadCreators()
    {
        WindowManager.OpenWindow<CreatorsWindow>();
    }

    private void LoadQuit()
    {
        WindowManager.OpenWindow<QuitWindow>();
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveListener(LoadCharacterSelect);
        _settingsButton.onClick.RemoveListener(LoadSettings);
        _creatorsButton.onClick.RemoveListener(LoadCreators);
        _quitButton.onClick.RemoveListener(LoadQuit);
    }
}