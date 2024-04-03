// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Pause;

public class PauseMenu : Window, IPauseHandler
{
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Toggle _soundToggle;
    [SerializeField] private Toggle _musicToggle;
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private Slider _musicSlider;

    public AudioMixerGroup MixerVolume;
    private const float OnSoundVariable = 0f;
    private const float OffSoundVariable = -80f;

    private const string MusicMixer = "MusicVolume";
    private const string SoundMixer = "SoundVolume";

    private const float Duration = 0.4f;

    private void Awake()
    {
        _restartButton.onClick.AddListener(RestartScene);
        _resumeButton.onClick.AddListener(ResumeGame);

        _musicToggle.onValueChanged.AddListener(OnMusicToggleValueChanged);
        _soundToggle.onValueChanged.AddListener(OnSoundToggleValueChanged);

        _musicSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
        _soundSlider.onValueChanged.AddListener(OnSoundSliderValueChanged);

        _quitButton.onClick.AddListener(LoadMainMenu);
    }

    private void OnEnable()
    {
        MixerVolume.audioMixer.GetFloat("MusicVolume", out var musicVolume);
        MixerVolume.audioMixer.GetFloat("SoundVolume", out var soundVolume);
        _musicSlider.value = musicVolume;
        _soundSlider.value = soundVolume;
    }

    private void ResumeGame()
    {
        transform.DOScale(Vector3.zero, Duration).SetAutoKill(true).OnComplete(WindowManager.CloseLast)
            .SetAutoKill(true);
        PauseManager.Instance.SetPaused(false);
    }

    private void RestartScene()
    {
        GameLogicController.RestartScene();
    }

    private void OnMusicSliderValueChanged(float value)
    {
        MixerVolume.audioMixer.SetFloat(MusicMixer, value);
    }

    private void OnSoundSliderValueChanged(float value)
    {
        MixerVolume.audioMixer.SetFloat(SoundMixer, value);
    }

    private void OnMusicToggleValueChanged(bool isOn)
    {
        var volume = isOn ? OnSoundVariable : OffSoundVariable;
        MixerVolume.audioMixer.SetFloat("MusicVolume", volume);
    }

    private void OnSoundToggleValueChanged(bool isOn)
    {
        var volume = isOn ? OnSoundVariable : OffSoundVariable;
        MixerVolume.audioMixer.SetFloat("SoundVolume", volume);
    }

    private void LoadMainMenu()
    {
        PauseManager.Instance.SetPaused(false);
        SceneManager.LoadScene("MainMenu");
    }

    public void SetPaused(bool isPaused)
    {
    }

    private void OnDestroy()
    {
        _restartButton.onClick.RemoveListener(RestartScene);
        _resumeButton.onClick.RemoveListener(ResumeGame);

        _musicToggle.onValueChanged.RemoveListener(OnMusicToggleValueChanged);
        _soundToggle.onValueChanged.RemoveListener(OnSoundToggleValueChanged);

        _musicSlider.onValueChanged.RemoveListener(OnMusicSliderValueChanged);
        _soundSlider.onValueChanged.RemoveListener(OnSoundSliderValueChanged);
    }
}