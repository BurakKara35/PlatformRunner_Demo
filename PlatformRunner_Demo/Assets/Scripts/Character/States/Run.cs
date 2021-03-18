using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : State
{
    private Rigidbody _rigidbody;
    private Transform _transform;
    private Animator _animator;
    private float _speed;

    public Run(Rigidbody rigidbody, Transform transform, Animator animator, float speed)
    {
        this._rigidbody = rigidbody;
        this._transform = transform;
        this._animator = animator;
        this._speed = speed;
    }

    public void Enter()
    {
        _animator.SetBool("run", true);
        _transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void PhysicsUpdate()
    {
        _rigidbody.velocity = new Vector3(0, 0, Time.fixedDeltaTime * _speed);
    }
}
