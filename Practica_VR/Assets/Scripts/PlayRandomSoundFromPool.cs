using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomSoundFromPool : MonoBehaviour
{
    [SerializeField] GameObject audioSourceParent;
    AudioSource[] audioSources;
    [SerializeField] AudioClip[] audios;


    void Start()
    {
        audioSources = audioSourceParent.GetComponentsInChildren<AudioSource>();
    }

    //Función que reproduce un audio aleatorio por varios audio sources
    public void PlayRandomSound(bool overrideSound)
    {
        if (!audioSources[0].isPlaying || overrideSound)
        {
            int randomIndex = Random.Range(0, audios.Length);
            foreach(AudioSource src in audioSources)
            {
                src.clip = audios[randomIndex];
                src.Play();
            }
        }
    }

    //Función que hace sonar a las gafas al chocar con una pared
    private void OnCollisionEnter(Collision collision)
    {
        PlayRandomSound(false);
    }
}
