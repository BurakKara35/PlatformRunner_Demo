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

    Character character;

    private void Awake()
    {
        character = GetComponent<Character>();
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
            character.characterMovingSideState = Character.CharacterMovingSideState.None;
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
                character.characterMovingSideState = Character.CharacterMovingSideState.Left;
            else if (_differenceBetweenSwipePositions > 0)
                character.characterMovingSideState = Character.CharacterMovingSideState.Right;

            if (!_swipeFinished)
            {
                _swipeCoroutine = Swiping();
                StartCoroutine(_swipeCoroutine);
            }
        }
    }
}
