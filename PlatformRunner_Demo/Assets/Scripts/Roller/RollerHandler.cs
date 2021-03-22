using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerHandler : MonoBehaviour, Handler
{
    #region Input Variables
    private bool _swipe = false;
    private bool _swipeFinished = true;
    private float _swipeFirstPosition;
    private float _differenceBetweenSwipePositions;
    private float _swipingInSeconds = 0.1f;
    private IEnumerator _swipeCoroutine;
    #endregion

    private Roller _roller;

    // For rotation of roller
    private Vector3 _FirstVector;
    private Vector3 _LastVector;

    private void Awake()
    {
        _roller = GetComponent<Roller>();
    }

    public void Handle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _swipe = true;
            _swipeFirstPosition = Input.mousePosition.y;
            _FirstVector = Input.mousePosition;
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
            _roller.rollerState = Roller.RollerStates.Wait;

            if (_swipeCoroutine != null)
                StopCoroutine(_swipeCoroutine);
        }
    }

    private IEnumerator Swiping()
    {
        yield return new WaitForSeconds(_swipingInSeconds);
        if (_swipe)
        {
            _differenceBetweenSwipePositions = Input.mousePosition.y - _swipeFirstPosition;
            _LastVector = Input.mousePosition;

            if (_differenceBetweenSwipePositions < 0)
                _roller.rollerState = Roller.RollerStates.Down;
            else if (_differenceBetweenSwipePositions > 0)
                _roller.rollerState = Roller.RollerStates.Up;

            if (!_swipeFinished)
            {
                _swipeCoroutine = Swiping();
                StartCoroutine(_swipeCoroutine);
            }
        }
    }

    public float Angle()
    {
        float angle = Mathf.Atan2(_LastVector.y - _FirstVector.y, _LastVector.x - _FirstVector.x) * 180 / Mathf.PI;
        return angle - 90;
    }
}
