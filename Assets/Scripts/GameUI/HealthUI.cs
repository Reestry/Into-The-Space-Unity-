// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameUI
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;
        [SerializeField] private TMP_Text _healthCount;

        private float _healthFill;

        private const int MaxHealth = 100;

        private void Awake()
        {
            PlayerController.HealthUpdated += HealthUpdated;
        }

        private void Start()
        {
            _healthFill = 1f;
        }

        private void HealthUpdated(float newHealth)
        {
            _healthFill = newHealth / MaxHealth;
            _healthBar.fillAmount = _healthFill;
            _healthCount.text = $"{_healthFill * MaxHealth}%";
        }

        private void OnDestroy()
        {
            PlayerController.HealthUpdated -= HealthUpdated;
        }
    }
}