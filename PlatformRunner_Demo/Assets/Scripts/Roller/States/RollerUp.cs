using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerUp : State
{
    private Rigidbody _rigidbody;
    private Transform _transform;
    private Quaternion _rotation;

    public RollerUp(Rigidbody rigidbody, Transform transform, Quaternion rotation)
    {
        this._rigidbody = rigidbody;
        this._transform = transform;
        this._rotation = rotation;
    }

    public void Enter()
    {

    }

    public void PhysicsUpdate()
    {
        _rigidbody.velocity = Vector3.up;
        _transform.rotation = _rotation;
    }
}