// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameUI
{
    public class UltimateUI : MonoBehaviour
    {
        [SerializeField] private Image _ultimateBar;
        [SerializeField] private TMP_Text _ultimateCount;

        private float _ultimateFill;

        private const int MaxUltimate = 100;

        private void Awake()
        {
            GameLogicController.UltimateUpdate += UltimateUpdate;
        }

        private void Start()
        {
            _ultimateFill = 0f;
        }

        private void UltimateUpdate(float newUltimate)
        {
            _ultimateFill = newUltimate / MaxUltimate;
            _ultimateBar.fillAmount = _ultimateFill;
            _ultimateCount.text = $"{_ultimateFill * MaxUltimate}%";
        }

        private void OnDestroy()
        {
            GameLogicController.UltimateUpdate -= UltimateUpdate;
        }
    }
}