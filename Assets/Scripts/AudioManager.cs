using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource AmbienceSource;
    [SerializeField] AudioSource SFXsource;
    [SerializeField] AudioSource FootStepsource;

    [Space]

    public AudioClip backgroundSFX;
    public AudioClip weatherSFX;
    public AudioClip deathSFX;
    public AudioClip wallSlideSFX;
    public AudioClip jumpSFX;
    public AudioClip landSFX;
    public AudioClip dashSFX;
    public AudioClip hitSFX;
    public AudioClip startGameSFX;



    public void Awake()
    {
        instance = this;
        musicSource.clip = backgroundSFX;
        musicSource.Play();

        AmbienceSource.clip = weatherSFX;
        AmbienceSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXsource.PlayOneShot(clip);
    }

}
