﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    [System.Serializable]
    class TimedEvent
    {
        public float time = 0f;
        public UnityEvent events;
    }

    [SerializeField] TimedEvent[] timedEvents;

    public void StartCountdown()
    {
        StartCoroutine("CallTimedEvents");
    }

    IEnumerator CallTimedEvents()
    {
        for(int i = 0; i < timedEvents.Length; i++)
        {
            yield return new WaitForSeconds(timedEvents[i].time);
            timedEvents[i].events.Invoke();
        }
    }
}