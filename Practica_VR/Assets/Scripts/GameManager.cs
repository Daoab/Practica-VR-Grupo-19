using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform targetPointParent;
    Transform[] targetPoints;

    [SerializeField] GameObject canPrefab;
    [SerializeField] GameObject googlesPrefab;
    [SerializeField] float force = 100f;
    [SerializeField] float torque = 5f;


    //Esta función genera todas las latas y las gafas en los target points con una dirección y velocidad aleatorias
    void Start()
    {
        // Va a pillar el transform del parent también, debe estar en lugar de spawn válido
        targetPoints = targetPointParent.GetComponentsInChildren<Transform>();

        Rigidbody rb = canPrefab.GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(Random.Range(-force, force), Random.Range(-force, force), Random.Range(-force, force)));
        rb.AddTorque(new Vector3(Random.Range(-torque, torque), Random.Range(-torque, torque), Random.Range(-torque, torque)));

        int googleIndex = Random.Range(1, targetPoints.Length);
        googlesPrefab.transform.position = targetPoints[googleIndex].transform.position;
        Rigidbody googlesRb = googlesPrefab.GetComponent<Rigidbody>();
        googlesRb.AddForce(new Vector3(Random.Range(-force, force), Random.Range(-force, force), Random.Range(-force, force)));
        googlesRb.AddTorque(new Vector3(Random.Range(-torque, torque), Random.Range(-torque, torque), Random.Range(-torque, torque)));

        for (int i = 1; i < targetPoints.Length; i++)
        {
            if (i != googleIndex)
            {
                rb = GameObject.Instantiate(canPrefab, targetPoints[i].position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.gameObject.transform.parent = transform;

                rb.AddForce(new Vector3(Random.Range(-force, force), Random.Range(-force, force), Random.Range(-force, force)));
                rb.AddTorque(new Vector3(Random.Range(-torque, torque), Random.Range(-torque, torque), Random.Range(-torque, torque)));
            }
        }
    }
}