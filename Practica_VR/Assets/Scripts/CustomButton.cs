using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomButton : MonoBehaviour
{
    [SerializeField] Rigidbody button;
    [SerializeField] float force = 10f;

    [SerializeField] UnityEvent onButtonDown;
    [SerializeField] UnityEvent onButtonUp;

    [SerializeField] float failsafeDistance = 0.3f;
    Vector3 buttonStartPos = Vector3.zero;
    Vector3 buttonStartPosLocal = Vector3.zero;

    private void Start()
    {
        buttonStartPos = button.transform.position;
        buttonStartPosLocal = button.transform.localPosition;
    }

    private void LateUpdate()
    {
        button.AddForce(button.transform.up * force);

        button.transform.localPosition = new Vector3(buttonStartPosLocal.x, button.transform.localPosition.y, buttonStartPosLocal.z);

        if ((button.transform.position - buttonStartPos).magnitude > failsafeDistance)
        {
            button.transform.position = buttonStartPos;
            button.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        onButtonDown.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        onButtonUp.Invoke();
    }
}
