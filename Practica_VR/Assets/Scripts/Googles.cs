using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Googles : MonoBehaviour
{
    OVRGrabbable grabbable;
    [SerializeField] float distanceToHead = 0.4f;
    [SerializeField] Transform juanCamera;
    [SerializeField] UnityEvent onGooglesPicked;

    // Start is called before the first frame update
    void Start()
    {
        grabbable = GetComponent<OVRGrabbable>();
    }

    void Update()
    {
        if (grabbable.isGrabbed && (transform.position - juanCamera.transform.position).magnitude < distanceToHead)
        {
            GetComponent<OVRGrabbable>().grabbedBy.GrabEndEvent();
            onGooglesPicked.Invoke();
        }
    }
}
