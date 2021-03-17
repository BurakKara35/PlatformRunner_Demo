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
    [HideInInspector] private Animator animator;
    [HideInInspector] public GameManager game;

    public float runSpeed;
    public float moveSideSpeed;

    public enum CharacterMovingSideState { None, Left, Right }
    [HideInInspector] public CharacterMovingSideState characterMovingSideState;

    private enum Event { Enter, Update }
    private Event _event;

    private static Vector3 _startingPoint;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        _handler = GetComponent<Handler>();

        _startingPoint = transform.position;
    }

    private void Start()
    {
        InitializeGame();

        Invoke("TempStartGame", 3f);
    }

    void TempStartGame()
    {
        game.gameState = GameManager.GameState.On;
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
        //else if (_event == Event.Update)
            //_currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        if (_event == Event.Update)
            _currentState.PhysicsUpdate();
    }

    private void StateMachine()
    {
        if (game.gameState == GameManager.GameState.Off)
        {
            _newState = new Idle(rigidbody, transform, animator);
        }
        else
        {
            _handler.Handle();

            if (characterMovingSideState == CharacterMovingSideState.Left)
                _newState = new MoveLeft(rigidbody, transform, moveSideSpeed, runSpeed);
            else if (characterMovingSideState == CharacterMovingSideState.Right)
                _newState = new MoveRight(rigidbody, transform, moveSideSpeed, runSpeed);
            else
                _newState = new Run(rigidbody, transform, animator, runSpeed);
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
        transform.position = _startingPoint;
        _currentState = new Idle(rigidbody, transform, animator);
        _event = Event.Enter;
    }
}
