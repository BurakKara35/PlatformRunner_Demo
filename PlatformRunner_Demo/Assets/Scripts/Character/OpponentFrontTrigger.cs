using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentFrontTrigger : MonoBehaviour
{
    [HideInInspector] public GameObject obstacleOnTheWay;
    [HideInInspector] public string informationObstacle;
    [HideInInspector] public GameObject rotatingPlatform;

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
            rotatingPlatform = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == obstacleOnTheWay)
        {
            obstacleOnTheWay = null;
            informationObstacle = "";
        }

        if (other.gameObject == rotatingPlatform)
            rotatingPlatform = null;
    }
}
