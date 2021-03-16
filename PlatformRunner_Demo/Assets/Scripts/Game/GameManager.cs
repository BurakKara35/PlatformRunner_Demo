using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private CameraController _camera;

    private void Awake()
    {
        _camera = Camera.main.GetComponent<CameraController>();
    }

    private void FixedUpdate()
    {
        _camera.FollowTarget();
    }
}
