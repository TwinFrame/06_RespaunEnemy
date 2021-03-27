using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class PlayingHumsterAudioClips : MonoBehaviour
{
    [SerializeField] private AudioClip[] _yeahClips;
    [SerializeField] private AudioClip[] _screamsClips;
    
    private int _numberOfClip;
    private AudioSource _audiosource;

    void Start()
    {
        _audiosource = GetComponent<AudioSource>();

        PlayClip(_yeahClips);
    }

    public void PlayHunsterClip()
    {
        PlayClip(_screamsClips, 0.9f);
    }

    private void PlayClip(AudioClip[] clips, float minPitchValue = 0.8f, float maxPitchValue = 1.1f)
    {
        _numberOfClip = Random.Range(0, clips.Length);

        _audiosource.pitch = Random.Range(minPitchValue, maxPitchValue);

        _audiosource.PlayOneShot(clips[_numberOfClip]);
    }
}
