using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : State
{
    private Rigidbody _rigidbody;
    private Animator _animator;
    private float _speed;

    public Run(Rigidbody rigidbody, Animator animator, float speed)
    {
        this._rigidbody = rigidbody;
        this._animator = animator;
        this._speed = speed;
    }

    public void Enter()
    {
        _animator.SetBool("run", true);
        Debug.Log("Run Enter");
    }

    public void LogicUpdate()
    {
        Debug.Log("Run Update");
    }

    public void PhysicsUpdate()
    {
        _rigidbody.velocity = new Vector3(0, 0, Time.fixedDeltaTime * _speed);
    }
}
