using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : State
{
    private Rigidbody _rigidbody;
    private Transform _transform;
    private float _speed;
    private float _runSpeed;

    public MoveRight(Rigidbody rigidbody, Transform transform, float speed, float runSpeed)
    {
        this._rigidbody = rigidbody;
        this._transform = transform;
        this._speed = speed;
        this._runSpeed = runSpeed;
    }

    public void Enter()
    {
        _transform.rotation = Quaternion.Euler(0, 45, 0);
    }

    public void LogicUpdate()
    {
        Debug.Log("Moving Right");
    }

    public void PhysicsUpdate()
    {
        float moveSide = Time.fixedDeltaTime * _speed;
        float run = Time.fixedDeltaTime * _runSpeed;

        _rigidbody.velocity = new Vector3(moveSide, 0, run);
    }
}
