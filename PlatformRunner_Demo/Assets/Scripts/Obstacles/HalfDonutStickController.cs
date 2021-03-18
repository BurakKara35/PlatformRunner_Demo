using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HalfDonutStickController : HorizontalMovingObstaclesBase
{
    private enum FacingSide { Left, Right}
    private FacingSide _facingSide;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _boundryInX = 0.145f;

        CheckObstacleFacingSide();
        ChooseDirectionFirst();
    }

    private void Update()
    {
        if (_facingSide == FacingSide.Left)
            CheckPosition(transform.localPosition.x);
        else
            CheckPosition(-transform.localPosition.x);
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void CheckObstacleFacingSide()
    {
        if (transform.parent.transform.eulerAngles.y == 180)
            _facingSide = FacingSide.Right;
        else
            _facingSide = FacingSide.Left;
    }
}
