using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform targetPointParent;
    Transform[] targetPoints;

    [SerializeField] GameObject canPrefab;
    [SerializeField] float force = 100f;
    [SerializeField] float torque = 5f;

    // Start is called before the first frame update
    void Start()
    {
        // Va a pillar el transforma del parent también, debe estar en lugar de spawn válido
        targetPoints = targetPointParent.GetComponentsInChildren<Transform>();

        Rigidbody rb = canPrefab.GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(Random.Range(-force, force), Random.Range(-force, force), Random.Range(-force, force)));
        rb.AddTorque(new Vector3(Random.Range(-torque, torque), Random.Range(-torque, torque), Random.Range(-torque, torque)));

        for (int i = 1; i < targetPoints.Length; i++)
        {
            rb = GameObject.Instantiate(canPrefab, targetPoints[i].position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.gameObject.transform.parent = transform;

            rb.AddForce(new Vector3(Random.Range(-force, force), Random.Range(-force, force), Random.Range(-force, force)));
            rb.AddTorque(new Vector3(Random.Range(-torque, torque), Random.Range(-torque, torque), Random.Range(-torque, torque)));
        }
    }
}
