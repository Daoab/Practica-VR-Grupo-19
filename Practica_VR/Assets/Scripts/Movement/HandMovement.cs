using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float force = 200f;
    [SerializeField] float angularForce = 0.8f;
    [SerializeField] float maxDistance = 1.0f;
    private Rigidbody rb;
    private Vector3 lastTargetPosition;
    

    [SerializeField] string grabButton = "Oculus_CrossPlatform_PrimaryHandTrigger";
    Animator anim;

    [SerializeField][Tooltip("Grabber de la mano contraria para saber si está sujetando un objeto interactuable")]
    OVRGrabber otherHandGrabber;

    //bool mustPoint = false;
    [SerializeField] bool rightHand = false;
    [SerializeField] string pointButton = "Oculus_CrossPlatform_SecondaryIndexTrigger";

    SphereCollider collider;

    Transform triggerObj;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        lastTargetPosition = target.position;
    }

    private void Update()
    {
        //mustPoint = false;
        transform.position += target.position - lastTargetPosition;
        Vector3 direction = target.position - transform.position;
        rb.AddForce(direction * force * Time.deltaTime, ForceMode.VelocityChange);
        Quaternion torque = Quaternion.Lerp(transform.rotation, target.rotation, angularForce);
        rb.MoveRotation(torque);


        if (Input.GetAxisRaw(grabButton) > 0f)
        {
            anim.SetBool("grab", true);
            anim.SetBool("idle", false);
        }
        else
        {
            anim.SetBool("grab", false);
            anim.SetBool("idle", true);
        }

        
    }

    private void LateUpdate()
    {
        lastTargetPosition = target.position;
        Vector3 direction = transform.position - target.position;
        if (direction.magnitude > maxDistance) {
            transform.position = target.position + direction.normalized * maxDistance; 
        }

        float distanceToTrigger = float.MaxValue;
        if (triggerObj != null)
        {
            distanceToTrigger = (transform.position - triggerObj.position).magnitude;
            if (distanceToTrigger > collider.radius)
            {
                triggerObj = null;
                distanceToTrigger = float.MaxValue;
            }
        }

        if (distanceToTrigger < collider.radius || (otherHandGrabber.grabbedObject != null && otherHandGrabber.grabbedObject.tag == "TouchScreen") || (rightHand && Input.GetAxisRaw(pointButton) > 0f))
            anim.SetBool("point", true);
        else
            anim.SetBool("point", false);

        


    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("TouchScreen") || other.tag.Equals("Button"))
            triggerObj = other.transform;
    }
}
