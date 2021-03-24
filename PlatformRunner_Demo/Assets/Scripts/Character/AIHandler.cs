using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHandler : MonoBehaviour, Handler
{
    private OpponentFrontTrigger _opponentFrontTrigger;
    [SerializeField] private GameObject _frontTrigger;

    private enum AIStates { Left, Right, Run }
    private AIStates _aIStates;

    private Character _character;

    private float _luckySide;

    #region AI Variables
    private float _xBoundryForStaticHandle;
    private float _xBoundryForRotatingPlatform;
    private float _xBoundryForRotator;
    private float _xClosureToHorizontalObstacleForManeuver = 1;
    #endregion

    private void Awake()
    {
        float _xBoundry = GameManager._xBoundry;

        _xBoundryForStaticHandle = _xBoundry - 4;
        _xBoundryForRotatingPlatform = _xBoundry - 8;
        _xBoundryForRotator = _xBoundry - 2;
        _xClosureToHorizontalObstacleForManeuver = 1;

        _opponentFrontTrigger = _frontTrigger.GetComponent<OpponentFrontTrigger>();
        _character = GetComponent<Character>();
        _luckySide = Random.Range(0, 2); // To use this, left and right should be the first two elements of enum.
    }

    public void Handle()
    {
        if (_opponentFrontTrigger.informationObstacle == "static")
            HandleStaticObstacle();
        else if (_opponentFrontTrigger.informationObstacle == "nonstatic")
            HandleNonStaticObstacle();
        else
            HandlePlatform();

        Process(_aIStates);
    }

    void HandleStaticObstacle()
    {
        float _xOfObject = _opponentFrontTrigger.obstacleOnTheWay.transform.position.x;

        if(_xOfObject > _xBoundryForStaticHandle)
            _aIStates = AIStates.Left;
        else if (_xOfObject < -_xBoundryForStaticHandle)
            _aIStates = AIStates.Right;
        else
        {
            if (_xOfObject > transform.position.x)
                _aIStates = AIStates.Left;
            else if (_xOfObject < transform.position.x)
                _aIStates = AIStates.Right;
            else
                _aIStates = (AIStates)_luckySide;
        }
    }

    void HandleNonStaticObstacle()
    {
        string _obstacleName = _opponentFrontTrigger.obstacleOnTheWay.name;

        if (_obstacleName.Contains("Rotating"))
        {
            HandleRotator();
        }
        else if (_obstacleName.Contains("Horizontal"))
        {
            HandleHorizontalObstacle();
        }
        else if (_obstacleName.Contains("Stick"))
        {
            HandleHalfDonut();
        }
    }

    private void HandlePlatform()
    {
        if (_opponentFrontTrigger.rotatingPlatform != null)
        {
            if (transform.position.x > _xBoundryForRotatingPlatform)
                _aIStates = AIStates.Left;
            else if (transform.position.x < -_xBoundryForRotatingPlatform)
                _aIStates = AIStates.Right;
            else
                _aIStates = AIStates.Run;
        }
        else
            _aIStates = AIStates.Run;
    }

    private void HandleHalfDonut()
    {
        var _objectSide = _opponentFrontTrigger.obstacleOnTheWay.GetComponent<HalfDonutStickController>()._obstacleMovingSide;

        if (transform.position.x > 0)
        {
            if(_objectSide == HorizontalMovingObstaclesBase.ObstacleMovingSide.Right)
            {
                _aIStates = AIStates.Left;
            }
            else
            {
                _aIStates = AIStates.Run;
            }
        }
        else
        {
            if (_objectSide == HorizontalMovingObstaclesBase.ObstacleMovingSide.Right)
            {
                _aIStates = AIStates.Right;
            }
            else
            {
                _aIStates = AIStates.Run;
            }
        }
    }

    private void HandleHorizontalObstacle()
    {
        var _objectSide = _opponentFrontTrigger.obstacleOnTheWay.GetComponent<HorizontalMovingObstaclesBase>()._obstacleMovingSide;

        if (_opponentFrontTrigger.obstacleOnTheWay.transform.position.z - transform.position.z < _xClosureToHorizontalObstacleForManeuver)
        {
            if (_objectSide == HorizontalMovingObstaclesBase.ObstacleMovingSide.Left)
                _aIStates = AIStates.Left;
            else
                _aIStates = AIStates.Right;
        }
        else
        {
            if (_objectSide == HorizontalMovingObstaclesBase.ObstacleMovingSide.Left)
                _aIStates = AIStates.Right;
            else
                _aIStates = AIStates.Left;
        }
    }

    private void HandleRotator()
    {
        float _transformX = transform.position.x;

        if (_transformX > _xBoundryForRotator || _transformX < -_xBoundryForRotator)
            _aIStates = AIStates.Run;
        else
        {
            if (_transformX > 0)
                _aIStates = AIStates.Right;
            else
                _aIStates = AIStates.Left;
        }
    }

    void Process(AIStates state)
    {
        if (_opponentFrontTrigger.obstacleOnTheWay != null)
        {
            if (state == AIStates.Left)
                _character.characterStates = Character.CharacterStates.Left;
            else if (state == AIStates.Right)
                _character.characterStates = Character.CharacterStates.Right;
            else
                _character.characterStates = Character.CharacterStates.Run;
        }
        else
        {
            if (_opponentFrontTrigger.rotatingPlatform != null)
            {
                if (state == AIStates.Left)
                    _character.characterStates = Character.CharacterStates.Left;
                else if (state == AIStates.Right)
                    _character.characterStates = Character.CharacterStates.Right;
            }
            else
                _character.characterStates = Character.CharacterStates.Run;
        }
    }
}
