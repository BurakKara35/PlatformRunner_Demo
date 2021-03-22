using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerDown : State
{
    private Rigidbody _rigidbody;
    private Transform _transform;
    private Quaternion _rotation;

    private float _speed;

    public RollerDown(Rigidbody rigidbody, Transform transform, Quaternion rotation, float speed)
    {
        this._rigidbody = rigidbody;
        this._transform = transform;
        this._rotation = rotation;
        this._speed = speed;
    }

    public void Enter()
    {

    }

    public void PhysicsUpdate()
    {
        _transform.rotation = _rotation;

        Vector3 down = -_transform.up;
        _rigidbody.velocity = down *_speed;
    }
}