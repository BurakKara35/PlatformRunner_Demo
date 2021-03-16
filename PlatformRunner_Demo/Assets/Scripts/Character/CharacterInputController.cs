using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class CharacterInputController : MonoBehaviour
{
    private State _runningState;

    private Rigidbody _rigidbody;

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

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _runningState = new Run(_rigidbody, runSpeed);
    }

    private void Update()
    {
        StateMachine();

        _runningState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        _runningState.PhysicsUpdate();
    }

    void StateMachine()
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
            _runningState = new Run(_rigidbody, runSpeed);
        }

        if (_characterMovingSideState == CharacterMovingSideState.Left)
            _runningState = new MoveLeft(_rigidbody, moveSideSpeed, runSpeed);
        else if (_characterMovingSideState == CharacterMovingSideState.Right)
            _runningState = new MoveRight(_rigidbody, moveSideSpeed, runSpeed);
    }

    public IEnumerator Swiping()
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
