using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : State
{
    private Rigidbody _rigidbody;

    public Stop(Rigidbody rigidbody)
    {
        this._rigidbody = rigidbody;
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
        Debug.Log("Stopped");
    }

    public void PhysicsUpdate()
    {
        _rigidbody.velocity = Vector3.zero;
    }
}
