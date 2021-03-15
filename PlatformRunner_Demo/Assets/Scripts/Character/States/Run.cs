using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : State
{
    private Rigidbody _rigidbody;
    private float _speed;

    public Run(Rigidbody rigidbody, float speed)
    {
        this._rigidbody = rigidbody;
        this._speed = speed;
    }

    public void Enter()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

    public void LogicUpdate()
    {
        Debug.Log("Running");
    }

    public void PhysicsUpdate()
    {
        _rigidbody.velocity = new Vector3(0, 0, Time.fixedDeltaTime * _speed);
    }
}
