// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private BulletSettings _bulletSettings;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public static event Action DamageTaken;

    public event Action<EnemyController> Died;

    private EnemyUI _enemyUI;
    private EnemyDamage _enemyDamage;

    private EnemyMovement _enemyMovement;
    private EnemyShooting _enemyShooting;

    private float _healthFill;
    private float _health;
    private float _currentMaxHealth;

    public void SetData(EnemyConfig enemyConfig, WaveConfig waveConfig)
    {
        _spriteRenderer.sprite = enemyConfig.Sprite;
        _health = enemyConfig.Health +
                  enemyConfig.Health * waveConfig.MagnificationFactor * WaveManager.HealthMultiplier;
        _currentMaxHealth = _health;

        _enemyMovement.SetMovementData(enemyConfig.Speed);
        _enemyShooting.SetShootingData(enemyConfig.ShootDelay, waveConfig.MagnificationFactor);
    }

    public async UniTask TakeDamage(float damage)
    {
        _health -= damage;

        DamageTaken?.Invoke();

        UpdateHealth(_health);
        _enemyDamage.TakingDamageAnimation();
        AudioManager.Instance.PlaySound("Explosion");

        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        HealthCheck();
    }

    private void Awake()
    {
        _enemyUI = GetComponent<EnemyUI>();
        _enemyDamage = GetComponent<EnemyDamage>();

        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyShooting = GetComponent<EnemyShooting>();
    }

    private void OnEnable()
    {
        _healthFill = 1f;
        _enemyUI.UpdateHealthBar(_healthFill);
    }

    private void UpdateHealth(float newHealth)
    {
        _healthFill = newHealth / (_currentMaxHealth);

        _enemyUI.UpdateHealthBar(_healthFill);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Projectile"))
            TakeDamage(_bulletSettings.Damage).Forget();
    }

    private void HealthCheck()
    {
        if (_health <= 0)
            Died?.Invoke(this);
    }
}