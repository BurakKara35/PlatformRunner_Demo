using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private float _arrangePainterTimeInSeconds = 2.5f;
    private float _scoreForGoodFeedback = 0.85f;

    [HideInInspector] public static float _xBoundry = 10; // (platform.scale.x * 0.8 ) / 2

    private void Awake()
    {
        Time.timeScale = 1;
        _camera = Camera.main.GetComponent<CameraController>();
        _ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIController>();
        _finalZ = GameObject.FindGameObjectWithTag("Final").gameObject.transform.position.z;
        gameState = GameState.Idle;
    }

    private void Update()
    {
        if ( gameState == GameState.Idle)
        {
            WaitForTap();
        }
        else if (gameState == GameState.Runner)
        {
            _camera.FollowTarget();
            _ui.RoadPlayerGet(_player.position.z / _finalZ);
        }
        else if (gameState == GameState.ArrangePainter)
        {
            ArrangementForPainterState();
            Invoke("StartPainterState", _arrangePainterTimeInSeconds);
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
        _ui.OpenFinishButton();
        gameState = GameState.Painter;
    }

    private void WaitForTap()
    {
        if (Input.GetMouseButtonDown(0))
            StartRunnerState();
    }

    private void StartRunnerState()
    {
        _ui.CloseIdleUI();
        _ui.OpenLevelUI();
        gameState = GameState.Runner;
    }

    public void GiveFeedback()
    {
        _ui.CloseFinishButton();
        _ui.CloseLevelUI();

        if ((float)_wallForegroundProcesses.touchedCountOfForeObject / (float)_wallForegroundProcesses.countOfForeObject > _scoreForGoodFeedback)
            _ui.OpenGoodFinishFeedback();
        else
            _ui.OpenBadFinishFeedback();

        _ui.OpenRestartButton();

        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
