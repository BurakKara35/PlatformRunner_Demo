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
    private float _startingXPoint;
    private float _difference;

    [HideInInspector] public int countOfForeObject;
    [HideInInspector] public int touchedCountOfForeObject;
    private int _totalCount;

    private void Awake()
    {
        _xPoint = -3.7f;
        _yPoint = 1.3f;
        _zPoint = _wall.position.z -0.001f;

        _startingXPoint = _xPoint;
        _difference = 0.2f;

        countOfForeObject = 0;
        touchedCountOfForeObject = 0;

        _totalCount = 38;

        InitializeForegroundObjects();
    }

    private void Start()
    {
        _wall.gameObject.SetActive(true);
    }

    private void InitializeForegroundObjects()
    {
        for (int i = 0; i < _totalCount; i++)
        {
            for (int j = 0; j < _totalCount; j++)
            {
                GameObject foreground = Instantiate(_foreground, new Vector3(_xPoint, _yPoint, _zPoint), Quaternion.identity);
                foreground.transform.parent = transform;
                _xPoint += _difference;

                countOfForeObject++;
            }
            _yPoint += _difference;
            _xPoint = _startingXPoint;
        }
    }
}
