// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestPool;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<WaveConfig> _waveConfig;

    public static WaveManager Instance { get; private set; }

    public static int HealthMultiplier = -1;

    public List<EnemyController> EnemiesOnScene = new();

    private int _waveIndex = -1;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        CheckForNextWave();
    }

    private void NextWave()
    {
        _waveIndex++;
        HealthMultiplier++;

        if (_waveIndex > _waveConfig.Count - 1)
            _waveIndex = 3;

        StartCoroutine(EnemySpawn(_waveIndex));
    }

    private void CheckForNextWave()
    {
        if (EnemiesOnScene.Count == 0)
            NextWave();
    }

    private IEnumerator EnemySpawn(int waveIndex)
    {
        var waveConfig = _waveConfig[waveIndex];
        var enemyConfigs = waveConfig.Wave.Keys;
        var enemyCounts = waveConfig.Wave.Values;

        for (var enemyType = 0; enemyType < enemyConfigs.Count; enemyType++)
        {
            var enemyConfig = enemyConfigs[enemyType];
            var enemyCount = enemyCounts[enemyType];

            for (var j = 0; j < enemyCount; j++)
            {
                var enemy = Pool.Get<EnemyController>();

                enemy.SetData(enemyConfig, waveConfig);
                enemy.Died += ReleaseEnemy;
                EnemiesOnScene.Add(enemy);
                enemy.transform.position = gameObject.transform.position;

                yield return new WaitForSeconds(waveConfig.WaveSpeedIncrementPercentage);
            }
        }

        yield return new WaitForSeconds(waveConfig.WaveSpeedIncrementPercentage);
    }

    private void ReleaseEnemy(EnemyController enemy)
    {
        enemy.Died -= ReleaseEnemy;
        EnemiesOnScene.Remove(enemy);
        Pool.Release(enemy);
        CheckForNextWave();
    }
}