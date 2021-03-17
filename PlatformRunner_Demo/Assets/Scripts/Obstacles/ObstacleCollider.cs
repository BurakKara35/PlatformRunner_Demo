using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ObstacleCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Character"))
        {
            Debug.Log("touched");
        }
    }
}
