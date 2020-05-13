using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCollisionDetector : MonoBehaviour
{
    [HideInInspector] public ArcadeManager manager;

    //Función que llama a GameOver del juego si el jugador choca con una tubería
    private void OnTriggerEnter2D(Collider2D collision)
    {
        manager.GameOver();
    }
}