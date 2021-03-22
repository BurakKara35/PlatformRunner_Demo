using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBase : MonoBehaviour
{
    #region Input Variables
    [HideInInspector] public bool swipe = false;
    [HideInInspector] public bool swipeFinished = true;
    [HideInInspector] public float swipeFirstPosition;
    [HideInInspector] public float differenceBetweenSwipePositions;
    [HideInInspector] public float swipingInSeconds = 0.1f;
    [HideInInspector] public IEnumerator swipeCoroutine;
    #endregion

    public void InputDown()
    {
        swipe = true;
    }

    public void InputContinues()
    {
        swipeFinished = false;
    }

    public void InputUp()
    {
        swipe = false;
        swipeFinished = true;
    }
}
