using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCollisionDetector : MonoBehaviour
{
    [HideInInspector] public ArcadeManager manager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        manager.GameOver();
    }
}
