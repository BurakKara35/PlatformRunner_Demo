using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Roller))]
public class RollerHandler : InputBase, Handler
{
    private Roller _roller;

    // For rotation of roller
    private Vector3 _firstVector;
    private Vector3 _lastVector;

    private void Awake()
    {
        _roller = GetComponent<Roller>();
    }

    public void Handle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InputDown();
            _firstVector = Input.mousePosition;
            swipeFirstPosition = _firstVector.y;
            swipeCoroutine = Swiping();
            StartCoroutine(swipeCoroutine);
        }
        if (Input.GetMouseButton(0) && swipe)
        {
            InputContinues();
        }
        if (Input.GetMouseButtonUp(0))
        {
            InputUp();
            _roller.rollerState = Roller.RollerStates.Wait;

            if (swipeCoroutine != null)
                StopCoroutine(swipeCoroutine);
        }
    }

    private IEnumerator Swiping()
    {
        yield return new WaitForSeconds(swipingInSeconds);
        if (swipe)
        {
            differenceBetweenSwipePositions = Input.mousePosition.y - swipeFirstPosition;
            _lastVector = Input.mousePosition;

            if (differenceBetweenSwipePositions < 0)
                _roller.rollerState = Roller.RollerStates.Down;
            else if (differenceBetweenSwipePositions > 0)
                _roller.rollerState = Roller.RollerStates.Up;

            if (!swipeFinished)
            {
                swipeCoroutine = Swiping();
                StartCoroutine(swipeCoroutine);
            }
        }
    }

    public float Angle()
    {
        float angle = Mathf.Atan2(_lastVector.y - _firstVector.y, _lastVector.x - _firstVector.x) * 180 / Mathf.PI;
        return angle - 90;
    }
}
