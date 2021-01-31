using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullAudio : MonoBehaviour
{
    [SerializeField]
    private float initialDelay = 5;
    [SerializeField]
    private float repeatingDelay = 15;

    [SerializeField]
    private AudioClip seagullAudio;

    AudioSource seagullAudioSource;

    private void Start()
    {
        seagullAudioSource = GetComponent<AudioSource>();
        InvokeRepeating("PlaySound", initialDelay, repeatingDelay);
    }

    void PlaySound()
    {
        seagullAudioSource.PlayOneShot(seagullAudio);
    }
}
