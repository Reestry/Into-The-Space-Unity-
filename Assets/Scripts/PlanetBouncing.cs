// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using DG.Tweening;

public class PlanetBouncing : MonoBehaviour
{
    [SerializeField] private float _endValue = 0.2f;
    [SerializeField] private float _duration = 1;

    private Tween _bounceTween;

    private void Start()
    {
        StartBounceAnimation();
    }

    private void StartBounceAnimation()
    {
        _bounceTween = transform.DOLocalMoveY(_endValue, _duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetAutoKill(true);
    }

    private void OnDestroy()
    {
        _bounceTween?.Kill();
    }
}