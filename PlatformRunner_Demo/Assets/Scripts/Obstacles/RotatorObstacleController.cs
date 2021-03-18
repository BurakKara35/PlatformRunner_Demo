using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorObstacleController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private float _speed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Rotation();
    }

    private void Rotation()
    {
        Quaternion temp = (Quaternion.Euler(new Vector3(0, 1, 0) * Time.fixedDeltaTime * _speed));
        _rigidbody.MoveRotation(transform.rotation * temp);
    }
}
