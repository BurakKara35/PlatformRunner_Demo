using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface State
{
    void Enter();

    void LogicUpdate();

    void PhysicsUpdate();

    void Exit();
}
