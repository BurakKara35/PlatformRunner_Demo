using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerTrigger : MonoBehaviour
{
    WallForegroundProcesses foregroundProcesses;

    private void Awake()
    {
        foregroundProcesses = GameObject.FindGameObjectWithTag("WallForegroundParent").GetComponent<WallForegroundProcesses>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WallForeground"))
        {
            other.gameObject.SetActive(false);
            foregroundProcesses.touchedCountOfForeObject++;
        }
    }
}
