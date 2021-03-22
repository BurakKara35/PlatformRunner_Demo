using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private CameraController _camera;
    private UIController _ui;
    private WallForegroundProcesses _wallForegroundProcesses;

    [HideInInspector] public enum GameState { Idle, Runner, ArrangePainter, Painter}
    [HideInInspector] public GameState gameState;

    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _paintingScene;
    [SerializeField] private GameObject _opponentParent;
    [SerializeField] private GameObject _normalPlatformParent;
    [SerializeField] private GameObject _rotatingPlatformParent;
    [SerializeField] private GameObject _obstacleParent;

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
            Invoke("StartPainterState", 2.5f);
            DestroyRunnerPlatform();
        }
        else if (gameState == GameState.Painter)
        {
            _ui.RoadPlayerGet((float)_wallForegroundProcesses.touchedCountOfForeObject / (float)_wallForegroundProcesses.countOfForeObject);
        }
    }

    private void ArrangementForPainterState()
    {
        _camera.LookAtWall(_paintingScene.transform.position.z);
    }

    private void DestroyRunnerPlatform()
    {
        _opponentParent.SetActive(false);
        _obstacleParent.SetActive(false);
        _rotatingPlatformParent.SetActive(false);
        _normalPlatformParent.SetActive(false);
    }

    private void StartPainterState()
    {
        _paintingScene.SetActive(true);
        _wallForegroundProcesses = GameObject.FindGameObjectWithTag("WallForegroundParent").GetComponent<WallForegroundProcesses>();
        gameState = GameState.Painter;
    }
}
