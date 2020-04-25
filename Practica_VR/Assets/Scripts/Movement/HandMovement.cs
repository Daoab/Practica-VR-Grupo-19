using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour
{
    [SerializeField] Transform targetPos;
    [SerializeField] float force = 0.5f;
    [SerializeField] float maxDistance = 1.0f;
    private Rigidbody rb;

    private Vector3 lastTargetPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastTargetPosition = targetPos.position;
    }
    
    // Update is called once per frame
    void Update()
    {
        //transform.position = targetPos.position;
        transform.position += targetPos.position - lastTargetPosition;
        Vector3 direction = targetPos.position - transform.position;
        //rb.velocity = direction * force; //force was 8
        rb.AddForce(direction * force * Time.deltaTime, ForceMode.VelocityChange);
    }

    private void LateUpdate()
    {
        lastTargetPosition = targetPos.position;
        Vector3 direction = transform.position - targetPos.position;
        if (direction.magnitude > maxDistance) {
            transform.position = targetPos.position + direction.normalized * maxDistance; 
        }
    }
}
