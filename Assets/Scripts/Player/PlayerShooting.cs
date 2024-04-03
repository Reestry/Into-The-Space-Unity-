// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using TestPool;
using Pause;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private CharacterSettings _characterSettings;
    [SerializeField] private BulletSettings _playerBullet;
    [SerializeField] private GameObject[] _guns;
    [SerializeField] private float _nextFireTime;

    private void Update()
    {
        if (PauseManager.IsPaused || !Input.GetMouseButton(0))
            return;

        if (Time.time < _nextFireTime)
            return;

        _nextFireTime = Time.time + _characterSettings.ShootDelay;
        FireProjectile();
    }

    private void FireProjectile()
    {
        foreach (var gun in _guns)
        {
            var bullet = Pool.Get<PlayerBullet>();

            bullet.transform.position = gun.transform.position;
            bullet.transform.rotation = gun.transform.rotation;

            Vector2 direction = gun.transform.up;

            bullet.SetDirection(direction, _playerBullet.Speed);

            AudioManager.Instance.PlaySound("Shoot1");
        }
    }
}