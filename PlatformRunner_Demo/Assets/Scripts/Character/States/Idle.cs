using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    private Rigidbody _rigidbody;
    private Transform _transform;
    private Animator _animator;

    public Idle(Rigidbody rigidbody, Transform transform, Animator animator)
    {
        this._rigidbody = rigidbody;
        this._transform = transform;
        this._animator = animator;
    }

    public void Enter()
    {
        _animator.SetBool("run", false);
        _transform.rotation = Quaternion.Euler(0, 0, 0);
        Debug.Log("Idle Enter");
    }

    public void Exit()
    {
        _animator.ResetTrigger("idle");
    }

    public void LogicUpdate()
    {
        Debug.Log("Idle Update");
    }

    public void PhysicsUpdate()
    {
        _rigidbody.velocity = Vector3.zero;
    }
}
