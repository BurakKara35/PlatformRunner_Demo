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

    private float _frameCount;
    private int _finalFrameCount;

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
        _frameCount = 0;
        _finalFrameCount = 10;
    }

    public void PhysicsUpdate()
    {
        // I use that way because i can't use Invoke or Coroutine here
        if (_frameCount < _finalFrameCount)
        {
            _rigidbody.AddForce((_transform.position - _obstaclePosition).normalized * _speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
            _frameCount++;
        }
        else if(_frameCount == _finalFrameCount)
        {
            _animator.SetTrigger("force_applied");
            _rigidbody.velocity = Vector3.zero;
            _frameCount++;
        }
    }
}
