using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GamerRoomTrigger : MonoBehaviour
{

    [SerializeField] UnityEvent onPlayerEnter;

    //En caso de que el jugador entre en el trigger se llama a su evento, que puede verse en el inspector
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") onPlayerEnter.Invoke();
    }
}
