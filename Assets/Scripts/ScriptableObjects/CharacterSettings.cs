// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSettings", menuName = "Game/Character Settings")]
public class CharacterSettings : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private GameObject _model;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private float _health;
    [SerializeField] private float _speed;
    [SerializeField] private float _shootDelay;

    public float UltimateMagnificationFactor => 2.5f;

    public string Name => _name;

    public GameObject Model => _model;

    public Sprite Sprite => _sprite;

    public float Health => _health;

    public float Speed => _speed;

    public float ShootDelay => _shootDelay;
}