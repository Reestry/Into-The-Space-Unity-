// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using DG.Tweening;

public class PlayerDamage : DamageAnimation
{
    public virtual void TakingDamageAnimation()
    {
        SpriteRenderer.material.DOColor(DamageColor, Duration)
            .SetEase(Ease.Flash)
            .OnComplete(() => SpriteRenderer.material.DOColor(NormalColor, Duration));
    }
}