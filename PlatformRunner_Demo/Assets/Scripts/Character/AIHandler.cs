using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHandler : MonoBehaviour, Handler
{
    private OpponentFrontTrigger _opponentFrontTrigger;
    [SerializeField] private GameObject _frontTrigger;

    private Character _character;

    private float _luckySide;

    private void Awake()
    {
        _opponentFrontTrigger = _frontTrigger.GetComponent<OpponentFrontTrigger>();
        _character = GetComponent<Character>();
        _luckySide = Random.Range(0, 2);
    }

    public void Handle()
    {
        if(_opponentFrontTrigger.informationObstacle == "static")
        {
            if(transform.position.x > 7)
                _character.characterStates = Character.CharacterStates.Left;
            else if(transform.position.x < -7)
                _character.characterStates = Character.CharacterStates.Right;
            else
            {
                if(_luckySide == 0)
                    _character.characterStates = Character.CharacterStates.Left;
                else
                    _character.characterStates = Character.CharacterStates.Right;
            }
        }
        else if(_opponentFrontTrigger.informationObstacle == "nonstatic")
        {
            if(_opponentFrontTrigger.obstacleOnTheWay.name.Contains("Rotating"))
            {
                if(transform.position.x < 7)
                    _character.characterStates = Character.CharacterStates.Right;
                else
                    _character.characterStates = Character.CharacterStates.Left;
            }
            else if(_opponentFrontTrigger.obstacleOnTheWay.name.Contains("Horizontal"))
            {
                if (_opponentFrontTrigger.obstacleOnTheWay.GetComponent<HorizontalMovingObstaclesBase>()._obstacleMovingSide == HorizontalMovingObstaclesBase.ObstacleMovingSide.Left)
                    _character.characterStates = Character.CharacterStates.Right;
                else
                    _character.characterStates = Character.CharacterStates.Left;
            }
        }
        else
        {
            if (_opponentFrontTrigger.rotatingPlatformSide == "left")
                _character.characterStates = Character.CharacterStates.Right;
            else if(_opponentFrontTrigger.rotatingPlatformSide == "right")
                _character.characterStates = Character.CharacterStates.Left;
            else
                _character.characterStates = Character.CharacterStates.Run;
        }
    }
}
