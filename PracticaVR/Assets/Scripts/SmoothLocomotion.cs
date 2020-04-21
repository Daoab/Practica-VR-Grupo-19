using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothLocomotion : MonoBehaviour
{
    Rigidbody rb;
    float xValue = 0f;
    float yValue = 0f;

    [SerializeField]
    float forceMultiplier = 100f;

    [SerializeField]
    Transform ovrCameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        xValue = Input.GetAxis("Oculus_GearVR_LThumbstickX");
        yValue = Input.GetAxis("Oculus_GearVR_LThumbstickY");
    }

    private void FixedUpdate()
    {
        Vector3 forceVector = new Vector3(xValue, 0f, yValue) * forceMultiplier;
        Vector3 direction = ovrCameraTransform.forward.normalized * forceVector.z 
            + ovrCameraTransform.right.normalized * forceVector.x;

        rb.AddForce(direction.x, 0f, direction.z);
    }
}
