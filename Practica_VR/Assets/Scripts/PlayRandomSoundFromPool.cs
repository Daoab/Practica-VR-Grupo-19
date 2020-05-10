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

    private void OnCollisionEnter(Collision collision)
    {
        PlayRandomSound(false);
    }
}
