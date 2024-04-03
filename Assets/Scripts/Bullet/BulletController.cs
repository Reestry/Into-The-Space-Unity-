// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using TestPool;
using Pause;

public abstract class BulletController : MonoBehaviour, IPauseHandler
{
    private Rigidbody2D _rigidbody;
    private Vector2 _velocity;

    public void SetPaused(bool isPaused)
    {
        _rigidbody.velocity = isPaused ? Vector2.zero : _velocity;
    }

    public void SetDirection(Vector2 direction, float speed)
    {
        _velocity = direction * speed;
        _rigidbody.velocity = _velocity;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        PauseManager.Register(this);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("InvisibleWall"))
            Pool.Release(this);
    }

    private void OnDestroy()
    {
        PauseManager.UnRegister(this);
    }
}