// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Configs/Wave")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] private SerializableDictionary<EnemyConfig, int> _wave;

    public SerializableDictionary<EnemyConfig, int> Wave => _wave;

    public float MagnificationFactor => 0.05f;

    public float WaveSpeedIncrementPercentage => 0.05f;
}