// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private BulletSettings _enemyBullet;

    public static event Action<float> HealthUpdated;

    public event Action<PlayerController> Died;

    private float _health;
    private PlayerDamage _playerDamage;

    private void OnEnable()
    {
        _health = GameLogicController.CharacterHealth;

        _playerDamage = GetComponent<PlayerDamage>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("EnemyProjectile"))
            TakeDamage();
    }

    private void TakeDamage()
    {
        _health -= _enemyBullet.Damage;
        HealthUpdated?.Invoke(_health);
        _playerDamage.TakingDamageAnimation();

        HealthCheck();
    }

    private void HealthCheck()
    {
        if (_health <= 0)
            Died?.Invoke(this);
    }
}