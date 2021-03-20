using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    private float _offset;
    private float _lerpingTime = 0.2f;

    private Vector3 _cameraToWallPosition;
    private Quaternion _cameraToWallRotation;

    private void Awake()
    {
        _offset = transform.position.z - target.transform.position.z;
        _cameraToWallRotation = Quaternion.Euler(0, 0, 0);
    }

    public void FollowTarget()
    {
        transform.position = Vector3.Lerp(transform.position, TargetPosition(), _lerpingTime);
    }

    private Vector3 TargetPosition()
    {
        return new Vector3(transform.position.x, transform.position.y, target.transform.position.z + _offset);
    }

    public void LookAtWall(float wallZ)
    {
        _cameraToWallPosition = new Vector3(0, 5, wallZ - 10);

        transform.position = Vector3.MoveTowards(transform.position, _cameraToWallPosition, 0.2f);
        transform.rotation = Quaternion.Lerp(transform.rotation, _cameraToWallRotation, 0.2f);
    }
}
