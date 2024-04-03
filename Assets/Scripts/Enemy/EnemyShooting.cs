// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using System.Collections;
using UnityEngine;
using Pause;
using TestPool;

public class EnemyShooting : MonoBehaviour, IPauseHandler
{
    [SerializeField] private BulletSettings _enemyBullet;
    [SerializeField] private Transform _gun;

    private float _shootDelay;
    private float _magnificationFactor;
    private Coroutine _shootCoroutine;
    private bool _canShoot;

    public void SetPaused(bool isPaused)
    {
        if (isPaused)
        {
            StopCoroutine(_shootCoroutine);
            _shootCoroutine = null;
            _canShoot = false;
        }
        else
        {
            _canShoot = true;
            _shootCoroutine = StartCoroutine(ShootCoroutine(_shootDelay));
        }
    }

    public void SetShootingData(float shootDelay, float magnificationFactor)
    {
        _shootDelay = shootDelay;
        _magnificationFactor = magnificationFactor;
        _canShoot = true;

        _shootCoroutine = StartCoroutine(ShootCoroutine(_shootDelay));
    }

    private void OnEnable()
    {
        PauseManager.Register(this);
    }

    private IEnumerator ShootCoroutine(float shootDelay)
    {
        yield return new WaitForSeconds(shootDelay);

        while (_canShoot)
        {
            FireProjectile();
            yield return new WaitForSeconds(shootDelay);
        }
    }

    private void FireProjectile()
    {
        var bullet = Pool.Get<EnemyBullet>();
        bullet.transform.position = _gun.transform.position;
        var currentSpeed = _enemyBullet.Speed * (1 + _magnificationFactor * WaveManager.HealthMultiplier);

        Vector2 direction = _gun.transform.up * -1.0f;
        bullet.SetDirection(direction, currentSpeed);

        AudioManager.Instance.PlaySound("Shoot2");
    }

    private void OnDisable()
    {
        if (_shootCoroutine != null)
            StopCoroutine(_shootCoroutine);

        PauseManager.UnRegister(this);
    }
}