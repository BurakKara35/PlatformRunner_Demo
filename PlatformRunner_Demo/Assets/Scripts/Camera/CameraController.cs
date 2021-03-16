using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    private float _offset;
    private float _lerpingTime = 0.2f; 

    private void Awake()
    {
        _offset = transform.position.z - target.transform.position.z;
    }

    public void FollowTarget()
    {
        transform.position = Vector3.Lerp(transform.position, TargetPosition(), _lerpingTime);
    }

    private Vector3 TargetPosition()
    {
        return new Vector3(transform.position.x, transform.position.y, target.transform.position.z + _offset);
    }
}
