using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseApp : MonoBehaviour
{
    [SerializeField] GameObject door;

    private void Start()
    {
        door.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
