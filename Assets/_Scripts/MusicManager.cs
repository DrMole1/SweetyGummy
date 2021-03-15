using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip musicMenu;
    public AudioClip musicGame;

    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusicMenu()
    {
        if(audioSource.clip == musicMenu)
        {
            return;
        }

        audioSource.clip = musicMenu;
        audioSource.Play(0);
    }

    public void PlayMusicGame()
    {
        audioSource.clip = musicGame;
        audioSource.Play(0);
    }
}