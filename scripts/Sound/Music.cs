using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {

    public AudioClip[] MusicList;
    public Transform Camera;

    void Start()
    {
        MusicList = new AudioClip[]
        {
            (AudioClip)Resources.Load("Sound/RangorStartSong")
        };

    }

    void Update()
    {
        GetComponent<AudioSource>().clip = MusicList[1];
        GetComponent<AudioSource>().Play();
    }
}
