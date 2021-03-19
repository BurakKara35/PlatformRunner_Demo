using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentFrontTrigger : MonoBehaviour
{
    [HideInInspector] public GameObject obstacleOnTheWay;
    [HideInInspector] public string informationObstacle;
    [HideInInspector] public string rotatingPlatformSide;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            obstacleOnTheWay = other.gameObject;

            if (obstacleOnTheWay.isStatic)
                informationObstacle = "static";
            else
                informationObstacle = "nonstatic";
        }

        if (other.gameObject.CompareTag("RotatingPlatform"))
            rotatingPlatformSide = other.transform.parent.GetChild(0).GetComponent<RotatingPlatformController>().MovingSide();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == obstacleOnTheWay)
        {
            obstacleOnTheWay = null;
            informationObstacle = "";
        }
    }
}
