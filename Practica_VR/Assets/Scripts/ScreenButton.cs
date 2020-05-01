using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScreenButton : MonoBehaviour
{
    [SerializeField] UnityEvent onButtonDown;
    [SerializeField][Tooltip("Si cualquier objeto puede interactuar con el botón o solo las manos")] bool onlyHands = true;
    int handLayer;

    private void Start()
    {
        handLayer = LayerMask.NameToLayer("Hands");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == handLayer || !onlyHands)
            onButtonDown.Invoke();
    }
}
