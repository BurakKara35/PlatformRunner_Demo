using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Handler))]
public class Character : MonoBehaviour
{
    private State _currentState;
    private State _newState;
    private Handler _handler;

    [HideInInspector] public Rigidbody rigidbody;
    [HideInInspector] public GameManager game;
    private Animator _animator;

    public float runSpeed;
    public float moveSideSpeed;

    [Header("Rotator Obstacle's Force")]
    public float appliedForceByRotator;

    public enum CharacterStates { Run, Left, Right, ForceApplied }
    [HideInInspector] public CharacterStates characterStates;

    private enum Event { Enter, Update }
    private Event _event;

    private Vector3 _startingPoint;

    private Vector3 _obstaclePositionThatAppliedForce;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        _handler = GetComponent<Handler>();

        _startingPoint = transform.position;
    }

    private void Start()
    {
        InitializeGame();
    }

    private void Update()
    {
        StateMachine();
        ControlIfStateChanged();

        if (_event == Event.Enter)
        {
            _currentState.Enter();
            _event = Event.Update;
        }
    }

    private void FixedUpdate()
    {
        if (_event == Event.Update)
            _currentState.PhysicsUpdate();
    }

    private void StateMachine()
    {
        if (game.gameState == GameManager.GameState.Idle)
        {
            _newState = new Idle(rigidbody, transform, _animator);
        }
        else
        {
            if (characterStates != CharacterStates.ForceApplied)
                _handler.Handle();

            if (characterStates == CharacterStates.Run)
                _newState = new Run(rigidbody, transform, _animator, runSpeed);
            else if (characterStates == CharacterStates.Left)
                _newState = new MoveLeft(rigidbody, transform, moveSideSpeed, runSpeed);
            else if (characterStates == CharacterStates.Right)
                _newState = new MoveRight(rigidbody, transform, moveSideSpeed, runSpeed);
            else if (characterStates == CharacterStates.ForceApplied)
                _newState = new ForceApplied(rigidbody, transform, _animator, appliedForceByRotator, ObstaclePositionThatAppliedForce);        
        }
    }

    private void ControlIfStateChanged()
    {
        if (_currentState.ToString() != _newState.ToString())
        {
            _currentState = _newState;
            _event = Event.Enter;
        }
    }

    public void InitializeGame()
    {
        _currentState = new Idle(rigidbody, transform, _animator);
        _event = Event.Enter;
        transform.position = _startingPoint;
    }

    public Vector3 ObstaclePositionThatAppliedForce
    {
        get { return _obstaclePositionThatAppliedForce; }
        set { _obstaclePositionThatAppliedForce = value; }
    }

    public void AppliedForceFinished()
    {
        characterStates = CharacterStates.Run;
    }
}