using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager S; // Singleton Definition

    private AudioSource audio;

    public AudioClip jumpClip;
    public AudioClip playerDeathClip;
    public AudioClip enemyDeathClip;


    private void Awake()
    {
        S = this; // singleton is assigned
    }

    // Start is called before the first frame update
    void Start()
    {
        // assign the audio source component
        audio = GetComponent<AudioSource>();
    }

    public void MakeJumpSound()
    {
        audio.PlayOneShot(jumpClip, 0.6f);
    }

    public void MakeEnemyDeathSound()
    {
        audio.PlayOneShot(enemyDeathClip, 0.6f);
    }

    public void MakePlayerDeathSound()
    {
        audio.PlayOneShot(playerDeathClip, 0.6f);
    }

}
