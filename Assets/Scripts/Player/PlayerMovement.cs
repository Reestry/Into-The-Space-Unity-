// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using Pause;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterSettings _characterSettings;

    private const float DirectionThreshold = 0.1f;

    private Rigidbody2D _rigidbody;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && !PauseManager.IsPaused)
            MovePlayer();
        else
            StopPlayer();
    }

    private void MovePlayer()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = -1f;

        var direction = mousePosition - transform.position;
        direction.y = 0f;

        if (!(direction.magnitude > DirectionThreshold))
            return;

        var force = direction.normalized * _characterSettings.Speed;
        _rigidbody.velocity = force;
    }

    private void StopPlayer()
    {
        _rigidbody.velocity = Vector2.zero;
    }
}