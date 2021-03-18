using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceApplied : State
{
    private Rigidbody _rigidbody;
    private Transform _transform;
    private Animator _animator;
    private float _speed;
    private Vector3 _obstaclePosition;

    private bool _isForceApplied;

    public ForceApplied(Rigidbody rigidbody, Transform transform, Animator animator, float speed, Vector3 obstaclePosition)
    {
        this._rigidbody = rigidbody;
        this._transform = transform;
        this._animator = animator;
        this._speed = speed;
        this._obstaclePosition = obstaclePosition;
    }

    public void Enter()
    {
        _isForceApplied = false;
        _animator.SetTrigger("force_applied");
    }

    public void PhysicsUpdate()
    {
        if (!_isForceApplied)
        {
            _rigidbody.AddForce((_transform.position - _obstaclePosition).normalized * _speed, ForceMode.VelocityChange);
            _isForceApplied = true;
        }
        else
            _rigidbody.velocity = Vector3.zero;
    }
}
