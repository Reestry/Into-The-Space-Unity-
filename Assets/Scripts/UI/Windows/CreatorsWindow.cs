// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UI;

public class CreatorsWindow : Window
{
    [SerializeField] private Button _backButton;

    private const float Duration = 0.4f;

    private void Awake()
    {
        _backButton.onClick.AddListener(Close);
    }

    private void Close()
    {
        transform.DOScale(Vector3.zero, Duration).SetAutoKill(true).OnComplete(WindowManager.CloseLast)
            .SetAutoKill(true);
    }

    private void OnDestroy()
    {
        _backButton.onClick.RemoveListener(Close);
    }
}