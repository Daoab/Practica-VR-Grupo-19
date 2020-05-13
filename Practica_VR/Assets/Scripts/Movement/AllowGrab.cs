using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowGrab : MonoBehaviour
{
    [SerializeField] float maxDistance = 0.5f;
    [SerializeField] Transform hand;
    OVRGrabber grabber;

    private void Start()
    {
        grabber = GetComponent<OVRGrabber>();
    }

    //Función que impide al jugador coger un objeto si el virtual prob está muy lejos de la mano real
    private void Update()
    {
        Vector3 direction = transform.position - hand.position;
        if (direction.magnitude <= maxDistance && !grabber.enabled)
            grabber.enabled = true;
        else if (direction.magnitude > maxDistance && grabber.enabled)
            grabber.enabled = false;
    }
}
