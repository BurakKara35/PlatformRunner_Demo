using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallForegroundProcesses : MonoBehaviour
{
    [SerializeField] private GameObject _foreground;
    [SerializeField] private Transform _wall;

    private float _xPoint;
    private float _yPoint;
    private float _zPoint;

    [HideInInspector] public int countOfForeObject;
    [HideInInspector] public int touchedCountOfForeObject;

    private void Awake()
    {
        _xPoint = -3.7f;
        _yPoint = 1.3f;
        _zPoint = _wall.position.z -0.001f;

        countOfForeObject = 0;
        touchedCountOfForeObject = 0;

        InitializeForegroundObjects();
    }

    private void Start()
    {

        _wall.gameObject.SetActive(true);
    }

    private void InitializeForegroundObjects()
    {
        for (int i = 0; i < 38; i++)
        {
            for (int j = 0; j < 38; j++)
            {
                GameObject foreground = Instantiate(_foreground, new Vector3(_xPoint, _yPoint, _zPoint), Quaternion.identity);
                foreground.transform.parent = transform;
                _xPoint += 0.2f;

                countOfForeObject++;
            }
            _yPoint += 0.2f;
            _xPoint = -3.7f;
        }
    }
}
