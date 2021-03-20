using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCollider : MonoBehaviour
{
    private GameManager _game;

    private void Awake()
    {
        _game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            if (other.gameObject.name.Contains("Player"))
                _game.gameState = GameManager.GameState.ArrangePainter;

            other.gameObject.SetActive(false);
        }
    }
}
