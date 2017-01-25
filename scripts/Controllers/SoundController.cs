using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour
{
    public AudioClip teleportSound;
    public AudioClip seedOfRangor;
    public AudioClip mirror;
    public AudioClip pickup;

    float defaultVolume;

    // Use this for initialization
    void Start()
    {
        defaultVolume = GetComponent<AudioSource>().volume;
    }

    void playTeleportSound()
    {
        GetComponent<AudioSource>().volume = 1.0f;
        GetComponent<AudioSource>().PlayOneShot(teleportSound);
        GetComponent<AudioSource>().volume = defaultVolume;
    }

    void playSeedSound()
    {
        GetComponent<AudioSource>().volume = 1.0f;
        GetComponent<AudioSource>().PlayOneShot(seedOfRangor);
        GetComponent<AudioSource>().volume = defaultVolume;
    }

    void playMirrorSound()
    {
        GetComponent<AudioSource>().volume = 1.0f;
        GetComponent<AudioSource>().PlayOneShot(mirror);
        GetComponent<AudioSource>().volume = defaultVolume;
    }

    void playPickupSound()
    {
        GetComponent<AudioSource>().volume = 0.75f;
        GetComponent<AudioSource>().PlayOneShot(pickup);
        GetComponent<AudioSource>().volume = defaultVolume;
    }
}