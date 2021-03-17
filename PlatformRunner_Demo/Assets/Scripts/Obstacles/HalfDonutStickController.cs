using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HalfDonutStickController : HorizontalMovingObstaclesBase
{
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _boundryInX = 0.145f;

        ChooseDirectionFirst();
    }

    private void Update()
    {
        CheckPosition(transform.localPosition.x);
    }

    private void FixedUpdate()
    {
        Movement();
    }
}
