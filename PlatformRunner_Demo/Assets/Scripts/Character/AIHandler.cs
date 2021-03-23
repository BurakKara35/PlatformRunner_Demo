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

    private void Awake()
    {
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

        if(_xOfObject > 6)
            _aIStates = AIStates.Left;
        else if (_xOfObject < -6)
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
            if (transform.position.x > 7)
                _aIStates = AIStates.Right;
            else
                _aIStates = AIStates.Run;
        }
        else if (_obstacleName.Contains("Horizontal"))
        {
            var _objectSide = _opponentFrontTrigger.obstacleOnTheWay.GetComponent<HorizontalMovingObstaclesBase>()._obstacleMovingSide;

            if (_opponentFrontTrigger.obstacleOnTheWay.transform.position.z - transform.position.z < 1)
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
        else if (_obstacleName.Contains("Stick"))
        {
            if (transform.position.x > 0)
                _aIStates = AIStates.Left;
            else
                _aIStates = AIStates.Right;
        }
    }

    void HandlePlatform()
    {
        if (_opponentFrontTrigger.rotatingPlatform != null)
        {
            if (transform.position.x > 2)
                _aIStates = AIStates.Left;
            else if (transform.position.x < -2)
                _aIStates = AIStates.Right;
            else
                _aIStates = AIStates.Run;
        }
        else
            _aIStates = AIStates.Run;

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
