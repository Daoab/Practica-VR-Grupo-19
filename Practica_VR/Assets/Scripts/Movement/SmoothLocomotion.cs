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
        xValue = Input.GetAxis("Oculus_CrossPlatform_PrimaryThumbstickHorizontal");
        yValue = Input.GetAxis("Oculus_CrossPlatform_PrimaryThumbstickVertical");
    }

    //Función que en función del input del joystick izquierdo actualiza la posición del jugador de forma suave
    private void FixedUpdate()
    {
        Vector3 forceVector = new Vector3(xValue, 0f, yValue) * forceMultiplier;
        
        Vector3 direction = ovrCameraTransform.forward.normalized * forceVector.z 
            + ovrCameraTransform.right.normalized * forceVector.x;

        direction = Vector3.ProjectOnPlane(direction, Vector3.up).normalized * direction.magnitude;
        
        rb.AddForce(direction.x, 0f, direction.z);
    }
}
