using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomSoundFromPool : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip[] audios;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
    }

    public void PlayRandomSound()
    {
        int randomIndex = Random.Range(0, audios.Length);

        audioSource.clip = audios[randomIndex];

        if (!audioSource.isPlaying)
            audioSource.Play();
    }
}
