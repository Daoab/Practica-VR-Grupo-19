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

    private void Update()
    {
        //Debug.Log("Grabber activo: " + grabber.enabled);
        Vector3 direction = transform.position - hand.position;
        if (direction.magnitude <= maxDistance && !grabber.enabled)
            grabber.enabled = true;
        else if (direction.magnitude > maxDistance && grabber.enabled)
            grabber.enabled = false;
    }
}
