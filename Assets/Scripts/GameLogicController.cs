// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Pause;
using UI;

public class GameLogicController : MonoBehaviour
{
    [SerializeField] private BulletSettings _bulletSettings;

    public static bool LossGame;

    public static float CharacterHealth { get; private set; }

    public static event Action<float> UltimateUpdate;

    public static GameLogicController Instance;

    public readonly Vector3 PlayerSpawnPosition = new(0f, -3f, 0f);

    private static float CharacterUltimate { get; set; }

    private CharacterSettings _currentCharacter;
    private const float MaxUltimateValue = 100;
    private const int DeathDelay = 2000;

    private GameObject _player;
    private PlayerController _playerController;

    public static void RestartScene()
    {
        WindowManager.CloseLast();
        WaveManager.HealthMultiplier = -1;
        PauseManager.Instance.SetPaused(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        LossGame = false;
        EnemyController.DamageTaken += UltimateChange;
    }

    private void Start()
    {
        CameraBoundsCalculator.CalculateCameraBounds();

        CreateCharacter();

        _playerController = _player.GetComponent<PlayerController>();
        if (_playerController != null)
            _playerController.Died += OnPlayerDied;

        UltimateUpdate?.Invoke(CharacterUltimate);

        AudioManager.Instance.PlayMusic("Ambient");
        AudioManager.Instance.LoopMusic();
    }

    private async void OnPlayerDied(PlayerController obj)
    {
        _player.SetActive(false);
        LossGame = true;
        await Task.Delay(DeathDelay);

        AudioManager.Instance.FadeOut(5f);
        WindowManager.OpenWindow<DefeatMenu>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !LossGame)
        {
            if (!PauseManager.IsPaused)
                WindowManager.OpenWindow<PauseMenu>();
            else
                WindowManager.CloseLast();
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (!(CharacterUltimate >= MaxUltimateValue))
                return;

            AudioManager.Instance.PlaySound("Ultimate");
            var ultimateDamage = _bulletSettings.Damage * _currentCharacter.UltimateMagnificationFactor;

            foreach (var enemy in WaveManager.Instance.EnemiesOnScene)
                enemy.TakeDamage(ultimateDamage).Forget();

            CharacterUltimate -= 100;
            UltimateUpdate?.Invoke(CharacterUltimate);
        }
    }

    private void CreateCharacter()
    {
        var selectedCharacter = CharactersSelectManager.Instance.GetSelectedCharacter();

        if (selectedCharacter != null)
        {
            _currentCharacter = selectedCharacter;

            CharacterUltimate = 0f;
            CharacterHealth = _currentCharacter.Health;
            _player = Instantiate(_currentCharacter.Model, PlayerSpawnPosition, Quaternion.identity);
        }
        else
            Debug.LogWarning("No character selected");
    }

    private void UltimateChange()
    {
        if (!(CharacterUltimate < MaxUltimateValue))
            return;

        CharacterUltimate += 1;
        UltimateUpdate?.Invoke(CharacterUltimate);
    }

    private void OnDestroy()
    {
        EnemyController.DamageTaken -= UltimateChange;
    }
}