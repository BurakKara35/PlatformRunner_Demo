using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingStickCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            Character character = collision.gameObject.GetComponent<Character>();
            character.ObstaclePositionThatAppliedForce = new Vector3(collision.contacts[0].point.x, 0, collision.contacts[0].point.z); ;
            character.characterStates = Character.CharacterStates.ForceApplied;
        }
    }
}