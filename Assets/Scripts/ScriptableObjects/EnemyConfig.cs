// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Game/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private float _health;
    [SerializeField] private float _speed;
    [SerializeField] private float _shootDelay;

    public Sprite Sprite => _sprite;

    public float Health => _health;

    public float Speed => _speed;

    public float ShootDelay => _shootDelay;
}