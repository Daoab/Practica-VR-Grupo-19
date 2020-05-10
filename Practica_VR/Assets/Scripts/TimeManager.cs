using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    [System.Serializable]
    class TimedEvent
    {
        public float time = 0f;
        public UnityEvent events;
    }

    bool gameVictory = false;

    [SerializeField] UnityEvent onVictory;
    [SerializeField] TimedEvent[] timedEvents;

    public void WinGame()
    {
        gameVictory = true;
        onVictory.Invoke();
    }

    public void LoseGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartCountdown()
    {
        StartCoroutine("CallTimedEvents");
    }

    IEnumerator CallTimedEvents()
    {
        int i = 0;
        while (i < timedEvents.Length && !gameVictory)
        {
            yield return new WaitForSeconds(timedEvents[i].time);
            Debug.Log("NextTimeEvent");
            if(!gameVictory) timedEvents[i].events.Invoke();
            i++;
        }
    }
}