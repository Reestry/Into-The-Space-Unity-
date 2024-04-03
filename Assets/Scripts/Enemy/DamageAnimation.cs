// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;

public abstract class DamageAnimation : MonoBehaviour
{
    [SerializeField] protected Color NormalColor = Color.white;
    [SerializeField] protected Color DamageColor = Color.red;

    protected const float Duration = 0.15f;
    protected readonly Vector3 VFXPositionZ = new(0, 0, -5);
    protected SpriteRenderer SpriteRenderer;

    protected virtual void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void OnEnable()
    {
        SpriteRenderer.material.color = NormalColor;
    }
}