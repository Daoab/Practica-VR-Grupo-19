using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    [SerializeField] Transform teleportDst;
    [SerializeField] float maxDistance = 5f;
    [SerializeField] Transform rightHand;

    RaycastHit hit;
    int layerMask;

    void Start()
    {
        teleportDst.gameObject.SetActive(false);
        layerMask = LayerMask.NameToLayer("Floor");
    }

    //Función que controla el teletransporte del jugador
    void Update()
    {
        float button = Input.GetAxisRaw("Oculus_CrossPlatform_SecondaryIndexTrigger");

        if(button > 0f)
        {
            if (Physics.Raycast(rightHand.position, rightHand.forward, out hit, maxDistance))
            {
                if (hit.collider.gameObject.layer == layerMask) //Si el raycast choca contra el suelo
                {
                    teleportDst.gameObject.SetActive(true);
                    teleportDst.transform.position = hit.point;
                }
                else
                {
                    teleportDst.gameObject.SetActive(false);
                }
            }
            else
            {
                teleportDst.gameObject.SetActive(false);
            }
        }
        else if (teleportDst.gameObject.activeSelf) //En caso de que deba teletransportarse
        {
            teleportDst.gameObject.SetActive(false);
            transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        }
    }
}
