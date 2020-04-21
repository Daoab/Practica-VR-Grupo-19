using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePlayerCollider : MonoBehaviour
{
    [SerializeField] CapsuleCollider playerCollider;
    [SerializeField] Transform OVRCenterCamera;
    [SerializeField] Transform cube;

    void Update()
    {
        playerCollider.center = new Vector3(OVRCenterCamera.localPosition.x, playerCollider.center.y, OVRCenterCamera.localPosition.z);
        cube.position = gameObject.transform.localToWorldMatrix * new Vector4(playerCollider.center.x, playerCollider.center.y, playerCollider.center.z, 0f);
    }
}
