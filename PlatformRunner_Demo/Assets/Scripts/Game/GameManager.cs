using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private CameraController _camera;
    private UIController _ui;

    [HideInInspector] public enum GameState { On, Off}
    [HideInInspector] public GameState gameState;

    public Transform player;

    private float _finalZ;

    private void Awake()
    {
        _camera = Camera.main.GetComponent<CameraController>();
        _ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIController>();
        _finalZ = GameObject.FindGameObjectWithTag("Final").gameObject.transform.position.z;
        gameState = GameState.Off;
    }

    private void FixedUpdate()
    {
        _camera.FollowTarget();
        _ui.RoadPlayerGet(player.position.z / _finalZ);
    }

    private void DestroyAllComponents()
    {

    }
}
