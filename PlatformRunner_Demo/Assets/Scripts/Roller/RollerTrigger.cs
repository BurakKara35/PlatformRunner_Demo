using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerTrigger : MonoBehaviour
{
    private WallForegroundProcesses _foregroundProcesses;
    private Roller _roller;

    private void Awake()
    {
        _foregroundProcesses = GameObject.FindGameObjectWithTag("WallForegroundParent").GetComponent<WallForegroundProcesses>();
        _roller = GameObject.FindGameObjectWithTag("Roller").GetComponent<Roller>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WallForeground"))
        {
            if(_roller.rollerState != Roller.RollerStates.Wait)
            {
                other.gameObject.SetActive(false);
                _foregroundProcesses.touchedCountOfForeObject++;
            }
        }
    }
}
