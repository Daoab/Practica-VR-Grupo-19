using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comic : MonoBehaviour
{
    [SerializeField] GameObject door;

    private void Start()
    {
        //Al iniciarse el juego, la puerta de baño debe activarse
        door.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
