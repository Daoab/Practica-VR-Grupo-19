using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    [System.Serializable]
    //Esta clase guarda un evento y el tiempo que debe pasar hasta que es invocado
    class TimedEvent
    {
        public float time = 0f;
        public UnityEvent events;
    }

    bool gameVictory = false;

    [SerializeField] UnityEvent onVictory;
    [SerializeField] TimedEvent[] timedEvents;
    [HideInInspector] public float maxGameTime = 0f;
    
    private void Awake()
    {
        //Se calcula el tiempo que durará el juego
        for (int i = 1; i < timedEvents.Length - 1; i++) maxGameTime += timedEvents[i].time;
    }

    public void WinGame()
    {
        gameVictory = true;
        onVictory.Invoke();
    }

    public void LoseGame()
    {
        //En caso de perder, se reinicia el juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartCountdown()
    {
        StartCoroutine("CallTimedEvents");
    }

    //Esta corrutina llama a los eventos en orden esperando el tiempo indicado entre ellos
    IEnumerator CallTimedEvents()
    {
        int i = 0;
        while (i < timedEvents.Length && !gameVictory)
        {
            yield return new WaitForSeconds(timedEvents[i].time);
            if(!gameVictory) timedEvents[i].events.Invoke();
            i++;
        }
    }
}