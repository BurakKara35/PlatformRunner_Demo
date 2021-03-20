using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatformController : MonoBehaviour
{
    private enum PlatformMovingSide { Left, Right }
    private PlatformMovingSide _platformMovingSide;

    [SerializeField] private float _speed;

    RotatingPlatformProperties rotatingPlatformProperties;

    private void Awake()
    {
        rotatingPlatformProperties = transform.parent.parent.GetComponent<RotatingPlatformProperties>();
        ChooseDirection();
        SelectRandomMaterial();
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(0, 0, _speed * Time.fixedDeltaTime, Space.Self);
    }

    private void ChooseDirection()
    {
        float random = Random.Range(0, System.Enum.GetNames(typeof(PlatformMovingSide)).Length);
        _platformMovingSide = (PlatformMovingSide)random;

        if (_platformMovingSide == PlatformMovingSide.Left)
            _speed *= -1;
    }

    private void SelectRandomMaterial()
    {
        this.GetComponent<MeshRenderer>().material = rotatingPlatformProperties.materials[Random.Range(0, rotatingPlatformProperties.materials.Length)];
    }

    private void ApplyForceToCharacter(Rigidbody rigidbody)
    {
        rigidbody.AddForce(new Vector3(_speed,0,0), ForceMode.VelocityChange);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            ApplyForceToCharacter(collision.gameObject.GetComponent<Rigidbody>());
        }
    }
}
