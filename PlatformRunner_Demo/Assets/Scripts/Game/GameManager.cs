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
        gameState = GameState.ArrangePainter;
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
            Invoke("StarPainterState", 2.5f);
            DestroyRunnerPlatform();
        }
        else if (gameState == GameState.Painter)
        {
            Debug.Log("painter");
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

    private void StarPainterState()
    {
        _paintingScene.SetActive(true);
        gameState = GameState.Painter;
    }
}
