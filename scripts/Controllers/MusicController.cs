using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour
{
    public Transform will;
    public Transform deceit;

    public AudioSource preparationSource;
    public AudioSource calmSource;
    public AudioSource hecticSource;

    public bool usePrepMusic = false;

    void Start()
    {
        calmSource.enabled = !usePrepMusic;
        hecticSource.enabled = !usePrepMusic;
        preparationSource.enabled = usePrepMusic;
        calmSource.dopplerLevel = 0;
        hecticSource.dopplerLevel = 0;
    }

    void Update()
    {
        if (!will || !deceit)
        {
            if (GameObject.FindGameObjectWithTag("Will"))
                will = GameObject.FindGameObjectWithTag("Will").transform;
            if (GameObject.FindGameObjectWithTag("Deceit"))
                deceit = GameObject.FindGameObjectWithTag("Deceit").transform;
        }
        else
        {
            if (!preparationSource.isPlaying)
            {
                calmSource.enabled = true;
                hecticSource.enabled = true;
            }
            if (Vector3.Distance(will.position, deceit.position) <= 5)
            {
                calmSource.volume = Mathf.Lerp(calmSource.volume, 0, Time.deltaTime);
                hecticSource.volume = Mathf.Lerp(hecticSource.volume, 1, Time.deltaTime);
            }
            else
            {
                calmSource.volume = Mathf.Lerp(calmSource.volume, 1, Time.deltaTime);
                hecticSource.volume = Mathf.Lerp(hecticSource.volume, 0, Time.deltaTime);
            }
        }
    }
}
