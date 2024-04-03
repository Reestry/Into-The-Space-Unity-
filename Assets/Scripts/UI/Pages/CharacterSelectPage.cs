// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UI;

public class CharacterSelectPage : Page
{
    [SerializeField] private GameObject _content;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _playButton;
    [SerializeField] private CharacterPanelInfo _characterPanel;

    private static CharacterSelectPage _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
            _instance = this;

        _backButton.onClick.AddListener(LoadMainPage);
        _playButton.onClick.AddListener(LoadGame);
    }

    private void Start()
    {
        var characters = CharactersSelectManager.Instance.GetCharacterSettings();

        foreach (var character in characters)
        {
            var newPanel = Instantiate(_instance._characterPanel, _instance._content.transform, false);
            newPanel.SetData(character);
        }
    }

    private void LoadMainPage()
    {
        PageManager.OpenPage<MainPage>();
    }

    private void LoadGame()
    {
        if (CharactersSelectManager.Instance.SelectedCharacter == null)
            WindowManager.OpenWindow<ErrorWindow>();
        else
            SceneManager.LoadScene("Game");
    }

    private void OnDestroy()
    {
        _backButton.onClick.RemoveListener(LoadMainPage);
        _playButton.onClick.RemoveListener(LoadGame);
    }
}