using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HorizontalObstacleController : HorizontalMovingObstaclesBase
{
    private static float _positionY;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        boundryInX = GameManager._xBoundry;
        _positionY = transform.position.y;
        transform.position = new Vector3(0, _positionY, transform.position.z);

        ChooseDirectionFirst();
    }

    private void Update()
    {
        CheckPosition(transform.position.x);
    }

    private void FixedUpdate()
    {
        if (_obstacleMovingSide == ObstacleMovingSide.Left)
            MoveLeft();
        else
            MoveRight();
    }
}