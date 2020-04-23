using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePlayerCollider : MonoBehaviour
{
    [SerializeField] CapsuleCollider playerCollider;
    [SerializeField] Transform OVRCenterCamera;

    void Update()
    {
        playerCollider.center = new Vector3(OVRCenterCamera.localPosition.x, playerCollider.center.y, OVRCenterCamera.localPosition.z);
    }
}
