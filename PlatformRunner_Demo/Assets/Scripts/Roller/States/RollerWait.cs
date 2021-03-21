using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerWait : State
{
    private Rigidbody _rigidbody;

    public RollerWait(Rigidbody rigidbody)
    {
        this._rigidbody = rigidbody;
    }

    public void Enter()
    {

    }

    public void PhysicsUpdate()
    {
        _rigidbody.velocity = Vector3.zero;
    }
}