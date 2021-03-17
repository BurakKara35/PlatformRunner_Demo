using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HorizontalObstacleController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private float _boundryInX;
    private static float _positionY;
    [SerializeField] private float _speed;

    private enum ObstacleMovingSide { Left, Right }
    private ObstacleMovingSide _obstacleMovingSide;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _boundryInX = 10;
        _positionY = transform.position.y;
        transform.position = new Vector3(0, _positionY, transform.position.z);

        ChooseDirectionFirst();
    }

    private void Update()
    {
        HorizontalMovement();
    }

    private void FixedUpdate()
    {
        if (_obstacleMovingSide == ObstacleMovingSide.Left)
            MoveLeft();
        else
            MoveRight();
    }

    private void HorizontalMovement()
    {
        if(transform.position.x > _boundryInX)
        {
            _obstacleMovingSide = ObstacleMovingSide.Left;
        }
        else if(transform.position.x < -_boundryInX)
        {
            _obstacleMovingSide = ObstacleMovingSide.Right;
        }
    }

    private void MoveLeft()
    {
        Vector3 temp = new Vector3(-_boundryInX, 0, 0).normalized * Time.fixedDeltaTime * _speed;
        _rigidbody.MovePosition(transform.position + temp);
    }

    private void MoveRight()
    {
        Vector3 temp = new Vector3(_boundryInX, 0, 0).normalized * Time.fixedDeltaTime * _speed;
        _rigidbody.MovePosition(transform.position + temp);
    }

    private void ChooseDirectionFirst()
    {
        float random = Random.Range(0, System.Enum.GetNames(typeof(ObstacleMovingSide)).Length);
        _obstacleMovingSide = (ObstacleMovingSide)random;
    }
}