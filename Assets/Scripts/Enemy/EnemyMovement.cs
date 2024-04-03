// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using Pause;

public class EnemyMovement : MonoBehaviour, IPauseHandler
{
    private const float MovementTime = 3f;
    private Vector3 _targetPosition;
    private Rigidbody2D _rigidbody;
    private bool _isMoving;

    private float _speed;
    private Coroutine _moveCoroutine;

    public void SetPaused(bool isPaused)
    {
        if (isPaused)
        {
            _isMoving = false;
            _rigidbody.velocity = Vector2.zero;
            StopCoroutine(_moveCoroutine);
            _moveCoroutine = null;
        }
        else
            _moveCoroutine = StartCoroutine(EnemyMovementTimer());
    }

    public void SetMovementData(float speed)
    {
        _speed = speed;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _moveCoroutine = StartCoroutine(EnemyMovementTimer());
        PauseManager.Register(this);
    }

    private void FixedUpdate()
    {
        if (!_isMoving)
            return;

        var newPosition = Vector3.Lerp(transform.position, _targetPosition, _speed * Time.deltaTime);
        _rigidbody.MovePosition(newPosition);

        if (Vector3.Distance(transform.position, _targetPosition) < 0.5f)
            _isMoving = false;
    }

    private IEnumerator EnemyMovementTimer()
    {
        while (true)
        {
            MoveToRandomPosition();

            yield return new WaitForSeconds(MovementTime);
        }
    }

    private void MoveToRandomPosition()
    {
        var randomX = Random.Range(CameraBoundsCalculator.CameraMinX, CameraBoundsCalculator.CameraMaxX);
        var randomY = Random.Range(GameLogicController.Instance.PlayerSpawnPosition.y + 1,
            CameraBoundsCalculator.CameraMaxY - 1);

        _targetPosition = new Vector3(randomX, randomY, transform.position.z);
        _isMoving = true;
    }

    private void OnDisable()
    {
        if (_moveCoroutine != null)
            StopCoroutine(_moveCoroutine);

        PauseManager.UnRegister(this);
    }
}