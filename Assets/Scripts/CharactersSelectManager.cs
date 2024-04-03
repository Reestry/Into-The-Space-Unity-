// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using System.Collections.Generic;
using UnityEngine;

public class CharactersSelectManager : MonoBehaviour
{
    [SerializeField] private Characters _characters;

    public static CharactersSelectManager Instance;
    [HideInInspector] public CharacterSettings SelectedCharacter;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public List<CharacterSettings> GetCharacterSettings()
    {
        return _characters.Character;
    }

    public void SetSelectedCharacter(CharacterSettings character)
    {
        SelectedCharacter = character;
    }

    public CharacterSettings GetSelectedCharacter()
    {
        return SelectedCharacter;
    }
}