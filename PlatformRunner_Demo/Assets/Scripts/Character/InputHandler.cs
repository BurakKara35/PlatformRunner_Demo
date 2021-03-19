using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class InputHandler : MonoBehaviour, Handler
{

    #region Input Variables
    private bool _swipe = false;
    private bool _swipeFinished = true;
    private float _swipeFirstPosition;
    private float _differenceBetweenSwipePositions;
    private float _swipingInSeconds = 0.1f;
    private IEnumerator _swipeCoroutine;
    #endregion

    private Character _character;

    private void Awake()
    {
        _character = GetComponent<Character>();
    }

    public void Handle()
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
            _character.characterStates = Character.CharacterStates.Run;
            StopCoroutine(_swipeCoroutine);
        }
    }

    private IEnumerator Swiping()
    {
        yield return new WaitForSeconds(_swipingInSeconds);
        if (_swipe)
        {
            _differenceBetweenSwipePositions = Input.mousePosition.x - _swipeFirstPosition;

            if (_character.characterStates != Character.CharacterStates.ForceApplied) // REMAINDER !!! Find out a better way to check this.
            {
                if (_differenceBetweenSwipePositions < 0)
                    _character.characterStates = Character.CharacterStates.Left;
                else if (_differenceBetweenSwipePositions > 0)
                    _character.characterStates = Character.CharacterStates.Right;
            }
            else
            {
                _swipe = false;
                _swipeFinished = true;
            }

            if (!_swipeFinished)
            {
                _swipeCoroutine = Swiping();
                StartCoroutine(_swipeCoroutine);
            }
        }
    }
}
