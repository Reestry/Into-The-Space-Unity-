// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Characters", menuName = "Game/Characters")]
public class Characters : ScriptableObject
{
    [SerializeField] private List<CharacterSettings> _character;

    public List<CharacterSettings> Character => _character;
}
