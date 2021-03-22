using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HorizontalMovingObstaclesBase : MonoBehaviour
{
    [HideInInspector] public Rigidbody rigidbody;

    [HideInInspector] public float boundryInX;
    public float speed;

    public enum ObstacleMovingSide { Left, Right }
    [HideInInspector] public ObstacleMovingSide _obstacleMovingSide;

    public void CheckPosition(float x_Position)
    {
        if (x_Position > boundryInX)
        {
            _obstacleMovingSide = ObstacleMovingSide.Left;
        }
        else if (x_Position < -boundryInX)
        {
            _obstacleMovingSide = ObstacleMovingSide.Right;
        }
    }
    public void Movement()
    {
        if (_obstacleMovingSide == ObstacleMovingSide.Left)
            MoveLeft();
        else
            MoveRight();
    }

    public void MoveLeft()
    {
        Vector3 temp = new Vector3(-boundryInX, 0, 0).normalized * Time.fixedDeltaTime * speed;
        rigidbody.MovePosition(transform.position + temp);
    }

    public void MoveRight()
    {
        Vector3 temp = new Vector3(boundryInX, 0, 0).normalized * Time.fixedDeltaTime * speed;
        rigidbody.MovePosition(transform.position + temp);
    }

    public void ChooseDirectionFirst()
    {
        float random = Random.Range(0, System.Enum.GetNames(typeof(ObstacleMovingSide)).Length);
        _obstacleMovingSide = (ObstacleMovingSide)random;
    }
}
