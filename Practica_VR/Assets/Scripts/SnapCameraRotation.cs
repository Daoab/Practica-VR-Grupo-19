using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapCameraRotation : MonoBehaviour
{
    [SerializeField] float rotationDegrees = 30f;
    [SerializeField] float sensibility = 0.5f;
    float xValue = 0;
    bool canRotate = true;

    // Update is called once per frame
    void Update()
    {
        xValue = Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickHorizontal");
        float xValueAbs = Mathf.Abs(xValue);
        if (xValueAbs >= sensibility && canRotate)
        {
            canRotate = false;
            transform.Rotate(Vector3.up, rotationDegrees * xValue / xValueAbs);
        }
        else if (xValueAbs < sensibility && !canRotate)
            canRotate = true;
    }
}
