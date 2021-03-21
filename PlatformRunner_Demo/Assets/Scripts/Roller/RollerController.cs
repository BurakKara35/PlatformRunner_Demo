using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class RollerController : MonoBehaviour
{
    private Rigidbody _rigidbody; 

    private float _xBoundry;
    private float _yBoundryMin;
    private float _yBoundryMax;
    private float _zConstant;

    private State _currentState;
    private State _newState;
    private Handler _handler;

    public enum RollerStates { Wait, Up, Down }
    [HideInInspector] public RollerStates rollerState;

    private enum Event { Enter, Update }
    private Event _event;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _xBoundry = 3;
        _yBoundryMin = 0.5f;
        _yBoundryMax = 7.5f;
        _zConstant = 225.7f;

        _handler = GetComponent<Handler>();
    }

    private void Update()
    {
        ClampObjectToBoundaries();

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
        _handler.Handle();

        if (rollerState == RollerStates.Wait)
            _newState = new RollerWait(_rigidbody);
        else if (rollerState == RollerStates.Up)
            _newState = new RollerUp(_rigidbody, transform, transform.rotation);
        else if (rollerState == RollerStates.Down)
            _newState = new RollerDown(_rigidbody, transform, transform.rotation);
    }

    private void ControlIfStateChanged()
    {
        if (_currentState.ToString() != _newState.ToString())
        {
            _currentState = _newState;
            _event = Event.Enter;
        }
    }

    private void ClampObjectToBoundaries()
    {
        float xPosition = Mathf.Clamp(transform.position.x, -_xBoundry, _xBoundry);
        float yPosition = Mathf.Clamp(transform.position.y, _yBoundryMin, _yBoundryMax);
        transform.position = new Vector3(xPosition, yPosition, _zConstant);
    }
}