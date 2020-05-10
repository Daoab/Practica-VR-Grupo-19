using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Googles : MonoBehaviour
{
    OVRGrabbable grabbable;
    Rigidbody rb;
    [SerializeField] float distanceToHead = 0.4f;
    [SerializeField] Transform juanCamera;
    [SerializeField] UnityEvent onGooglesPicked;
    [SerializeField] float constantForce = 2.0f;
    [SerializeField] float minVelocity = 1.0f;

    Vector3 direction = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        grabbable = GetComponent<OVRGrabbable>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (grabbable.isGrabbed && (transform.position - juanCamera.transform.position).magnitude < distanceToHead)
        {
            GetComponent<OVRGrabbable>().grabbedBy.GrabEndEvent();
            onGooglesPicked.Invoke();
        }
    }

    private void LateUpdate()
    {
        if (rb.velocity.magnitude == 0.0f)
        {
            direction = new Vector3(Random.Range(0, 1), Random.Range(0, 1), Random.Range(0, 1)).normalized;
        }

        else
            direction = rb.velocity.normalized;

        if(rb.velocity.magnitude < minVelocity)
        {
            rb.velocity = direction * constantForce;
        }
    }
}
