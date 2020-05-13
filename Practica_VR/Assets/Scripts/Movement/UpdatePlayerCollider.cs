using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePlayerCollider : MonoBehaviour
{
    [SerializeField] CapsuleCollider playerCollider;
    [SerializeField] Transform OVRCenterCamera;

    //Función que actualiza el centro del collider del jugador a la posición de la cámara (para que se mantenga anclado a la cabeza)
    void Update()
    {
        playerCollider.center = new Vector3(OVRCenterCamera.localPosition.x, playerCollider.center.y, OVRCenterCamera.localPosition.z);
    }
}
