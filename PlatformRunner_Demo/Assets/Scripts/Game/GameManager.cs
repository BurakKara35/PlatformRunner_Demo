using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private CameraController _camera;

    public enum GameState { On, Off}
    public GameState gameState;

    private void Awake()
    {
        _camera = Camera.main.GetComponent<CameraController>();
        gameState = GameState.Off;
    }

    private void FixedUpdate()
    {
        _camera.FollowTarget();
    }

    void DestroyAllComponents()
    {

    }
}
