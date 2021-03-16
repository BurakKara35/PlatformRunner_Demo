using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class CharacterInputController : MonoBehaviour
{
    private State _currentState;
    private State _newState;

    private Rigidbody _rigidbody;
    private Animator _animator;

    public float runSpeed;
    public float moveSideSpeed;

    #region Input Variables
    private bool _swipe = false;
    private bool _swipeFinished = true;
    private float _swipeFirstPosition;
    private float _differenceBetweenSwipePositions;
    private float _swipingInSeconds = 0.1f;
    private IEnumerator _swipeCoroutine;
    #endregion

    private enum CharacterMovingSideState { None, Left, Right }
    private CharacterMovingSideState _characterMovingSideState;

    private enum Event { Enter, Update }
    private Event _event;

    private GameManager _game;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        _currentState = new Idle(_rigidbody, _animator);
        _event = Event.Enter;

        Invoke("TempStartGame", 3f);
    }

    void TempStartGame()
    {
        _game.gameState = GameManager.GameState.On;
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
        else if (_event == Event.Update)
            _currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        if (_event == Event.Update)
            _currentState.PhysicsUpdate();
    }

    private void StateMachine()
    {
        if(_game.gameState == GameManager.GameState.Off)
        {
            _newState = new Idle(_rigidbody, _animator);
        }
        else
        {
            InputHandler();

            if (_characterMovingSideState == CharacterMovingSideState.Left)
                _newState = new MoveLeft(_rigidbody, moveSideSpeed, runSpeed);
            else if (_characterMovingSideState == CharacterMovingSideState.Right)
                _newState = new MoveRight(_rigidbody, moveSideSpeed, runSpeed);
            else
                _newState = new Run(_rigidbody, _animator, runSpeed);
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

    private void InputHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _swipe = true;
            _swipeFirstPosition = Input.mousePosition.x;
            _swipeCoroutine = Swiping();
            StartCoroutine(_swipeCoroutine);
        }
        if (Input.GetMouseButton(0) && _swipe)
        {
            _swipeFinished = false;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _swipe = false;
            _swipeFinished = true;
            _characterMovingSideState = CharacterMovingSideState.None;
            StopCoroutine(_swipeCoroutine);
        }
    }

    private IEnumerator Swiping()
    {
        yield return new WaitForSeconds(_swipingInSeconds);
        if (_swipe)
        {
            _differenceBetweenSwipePositions = Input.mousePosition.x - _swipeFirstPosition;

            if (_differenceBetweenSwipePositions < 0)
                _characterMovingSideState = CharacterMovingSideState.Left;
            else if (_differenceBetweenSwipePositions > 0)
                _characterMovingSideState = CharacterMovingSideState.Right;

            if (!_swipeFinished)
            {
                _swipeCoroutine = Swiping();
                StartCoroutine(_swipeCoroutine);
            }
        }
    }
}
