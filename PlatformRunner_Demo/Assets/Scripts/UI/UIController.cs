using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image roadPlayerGet;

    public void RoadPlayerGet(float filledValue)
    {
        roadPlayerGet.fillAmount = filledValue;
    }
}
