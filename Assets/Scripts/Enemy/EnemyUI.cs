// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private float _smoothSpeed;

    private Tween _fillTween;

    public void UpdateHealthBar(float fill)
    {
        if (_fillTween != null && _fillTween.IsActive())
            _fillTween.Kill();

        _fillTween = _healthBar.DOFillAmount(fill, _smoothSpeed * Time.deltaTime);
    }
}