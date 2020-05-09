using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLights : MonoBehaviour
{

    Light light;

    private void Start()
    {
        light = GetComponent<Light>();
    }

    public void ToggleLight()
    {
        light.enabled = !light.enabled;
    }
}
