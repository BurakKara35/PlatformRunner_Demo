using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class InputHandler : InputBase, Handler
{
    private Character _character;

    private void Awake()
    {
        _character = GetComponent<Character>();
    }

    public void Handle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InputDown();
            swipeFirstPosition = Input.mousePosition.x;
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
            _character.characterStates = Character.CharacterStates.Run;
            
            if(swipeCoroutine != null)
                StopCoroutine(swipeCoroutine);
        }
    }

    private IEnumerator Swiping()
    {
        yield return new WaitForSeconds(swipingInSeconds);
        if (swipe)
        {
            differenceBetweenSwipePositions = Input.mousePosition.x - swipeFirstPosition;

            if (_character.characterStates != Character.CharacterStates.ForceApplied) // REMAINDER !!! Find out a better way to check this.
            {
                if (differenceBetweenSwipePositions < 0)
                    _character.characterStates = Character.CharacterStates.Left;
                else if (differenceBetweenSwipePositions > 0)
                    _character.characterStates = Character.CharacterStates.Right;
            }
            else
            {
                swipe = false;
                swipeFinished = true;
            }

            if (!swipeFinished)
            {
                swipeCoroutine = Swiping();
                StartCoroutine(swipeCoroutine);
            }
        }
    }
}