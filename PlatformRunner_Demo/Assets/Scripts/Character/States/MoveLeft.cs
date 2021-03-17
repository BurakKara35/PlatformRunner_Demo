using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : State
{
    private Rigidbody _rigidbody;
    private float _speed;
    private float _runSpeed;

    public MoveLeft(Rigidbody rigidbody, float speed, float runSpeed)
    {
        this._rigidbody = rigidbody;
        this._speed = speed;
        this._runSpeed = runSpeed;
}

    public void Enter()
    {

    }

    public void LogicUpdate()
    {
        Debug.Log("Moving Left");
    }

    public void PhysicsUpdate()
    {
        float moveSide = Time.fixedDeltaTime * _speed;
        float run = Time.fixedDeltaTime * _runSpeed;

        _rigidbody.velocity = new Vector3(-moveSide, 0, run);
    }
}
