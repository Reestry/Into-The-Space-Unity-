// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using DG.Tweening;
using TestPool;

public class EnemyDamage : DamageAnimation
{
    public virtual void TakingDamageAnimation()
    {
        var particle = Pool.Get<ParticleController>();
        particle.transform.position = transform.position + VFXPositionZ;

        SpriteRenderer.material.DOColor(DamageColor, Duration)
            .SetEase(Ease.Flash)
            .OnComplete(() => SpriteRenderer.material.DOColor(NormalColor, Duration));
    }
}