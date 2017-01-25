using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class WalkController : MonoBehaviour {

    public AudioClip walkSound;
    public GameObject dustCloudVertical;
    public GameObject dustCloudHorizontal;

    Animator animator;

    AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();

        GetComponent<AudioSource>().clip = walkSound;
        GetComponent<AudioSource>().pitch = 0.5f;
        GetComponent<AudioSource>().volume = 0.2f;
    }

	// Use this for initialization
	void OnEnable ()
    {
        AnimatorStateInfo stateInfo = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);

        if (stateInfo.nameHash == Animator.StringToHash("Base.Run_Up") || stateInfo.nameHash == Animator.StringToHash("Base.Run_Down"))
            Instantiate(dustCloudVertical, transform.position, transform.rotation);
        if (stateInfo.nameHash == Animator.StringToHash("Base.Run_Right") || stateInfo.nameHash == Animator.StringToHash("Base.Run_Left"))
            Instantiate(dustCloudHorizontal, transform.position, transform.rotation);
	}

    void VerticalStep()
    {
        GetComponent<AudioSource>().PlayOneShot(walkSound);
        Instantiate(dustCloudVertical, transform.position, transform.rotation);
    }

    void HorizontalStep()
    {
        GetComponent<AudioSource>().PlayOneShot(walkSound);
        Instantiate(dustCloudHorizontal, transform.position, transform.rotation);
    }
}
