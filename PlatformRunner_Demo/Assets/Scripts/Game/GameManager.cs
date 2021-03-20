using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private CameraController _camera;
    private UIController _ui;

    [HideInInspector] public enum GameState { Idle, Runner, ArrangePainter, Painter}
    [HideInInspector] public GameState gameState;

    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _wall;
    [SerializeField] private GameObject _opponentParent;

    private float _finalZ;

    private void Awake()
    {
        _camera = Camera.main.GetComponent<CameraController>();
        _ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIController>();
        _finalZ = GameObject.FindGameObjectWithTag("Final").gameObject.transform.position.z;
        gameState = GameState.Idle;
    }

    private void FixedUpdate()
    {
        if (gameState == GameState.Runner)
        {
            _camera.FollowTarget();
            _ui.RoadPlayerGet(_player.position.z / _finalZ);
        }
        else if (gameState == GameState.ArrangePainter)
        {
            ArrangementForPainterState();
            Invoke("Painter", 2.5f);
            DestroyAllComponents();
        }
        else if (gameState == GameState.Painter)
        {
            Debug.Log("painter");
        }
    }

    private void ArrangementForPainterState()
    {
        _camera.LookAtWall(_wall.transform.position.z);
    }

    private void DestroyAllComponents()
    {
        _opponentParent.SetActive(false);
    }

    private void Painter()
    {
        gameState = GameState.Painter;
    }
}
