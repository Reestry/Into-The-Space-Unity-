// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;

[CreateAssetMenu(fileName = "BulletSettings", menuName = "Game/Bullet Settings")]
public class BulletSettings : ScriptableObject
{
    [SerializeField] private float _damage;
    [SerializeField] private float _speed;

    public float Damage => _damage;

    public float Speed => _speed;
}