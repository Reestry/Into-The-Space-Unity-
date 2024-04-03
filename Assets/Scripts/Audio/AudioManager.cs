// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private SoundConfig _soundConfig;
    [SerializeField] private AudioSource _effectsAudioSource;
    [SerializeField] private AudioSource _musicAudioSource;

    public static AudioManager Instance;

    public void PlaySound(string soundName)
    {
        if (!_soundConfig.SoundDictionary.Keys.Contains(soundName))
            return;

        var soundClip = _soundConfig.SoundDictionary.Values[_soundConfig.SoundDictionary.Keys.IndexOf(soundName)];
        _effectsAudioSource.PlayOneShot(soundClip);
    }

    public void PlayMusic(string musicName)
    {
        if (_soundConfig.SoundDictionary.Keys.Contains(musicName))
        {
            var soundClip = _soundConfig.SoundDictionary.Values[_soundConfig.SoundDictionary.Keys.IndexOf(musicName)];
            _musicAudioSource.PlayOneShot(soundClip);
        }
        else
            Debug.LogWarning("Unknown name: " + musicName);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void FadeOut(float duration)
    {
        StartCoroutine(FadeOutCoroutine(duration));
    }

    private IEnumerator FadeOutCoroutine(float duration)
    {
        var startVolume = _musicAudioSource.volume;

        while (_musicAudioSource.volume > 0)
        {
            _musicAudioSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        _musicAudioSource.Stop();
        _musicAudioSource.volume = startVolume;
    }

    public void StopMusic()
    {
        _musicAudioSource.Stop();
    }

    public void LoopMusic()
    {
        _musicAudioSource.loop = true;
    }
}