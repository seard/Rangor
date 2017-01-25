using UnityEngine;
using System.Collections;

public class WinCondition : MonoBehaviour {

    public AudioClip winSound;
    public AudioClip loseSound;
    public UI2DSprite screenFader;
    public UI2DSprite bloodScreenFader;

    public bool gameOver = false;
    public bool endMusicPlaying;

    Transform thisPlayer;
    Transform thatPlayer;

    bool grabbing = false;

    void Start()
    {
        bloodScreenFader = GameObject.FindGameObjectWithTag("ScreenFader1").GetComponent<UI2DSprite>();
    }

	// Update is called once per frame
	void Update ()
    {
        if (!thisPlayer)
            if (GetNetPlayer.GetThisPlayer())
                thisPlayer = GetNetPlayer.GetThisPlayer();

        if (!thatPlayer)
            if (GetNetPlayer.GetThatPlayer())
                thatPlayer = GetNetPlayer.GetThatPlayer();

        if(grabbing)
        {
            bloodScreenFader.color = Color.Lerp(bloodScreenFader.color, new Color(0.8f, 0, 0, 0.5f), Time.deltaTime);
        }
        else
        {
            bloodScreenFader.color = Color.Lerp(bloodScreenFader.color, Color.clear, Time.deltaTime);
        }

        // When game is over
	    if(gameOver)
        {
            transform.FindChild("PlayerGraphics").GetComponent<Animator>().SetBool("GameOver", true);

            if (winSound && !endMusicPlaying)
            {
                GetComponent<AudioSource>().volume = 1.0f;
                if (tag == "Will")
                    GetComponent<AudioSource>().PlayOneShot(winSound);
                else
                    GetComponent<AudioSource>().PlayOneShot(loseSound);
                endMusicPlaying = true;
            }

            if (!screenFader)
                screenFader = GameObject.FindGameObjectWithTag("ScreenFader").GetComponent<UI2DSprite>();
            screenFader.color = Color.Lerp(screenFader.color, Color.white, Time.deltaTime / 2.0f);
            CameraShake.Shake(1.5f);
        }
	}

    [RPC]
    void IsGrabbing()
    {
        grabbing = true;
    }

    [RPC]
    void IsNotGrabbing()
    {
        grabbing = false;
    }

    [RPC]
    void GameOver()
    {
        gameOver = true;
        GetComponent<PlayerController>().lockControls = true;
    }
}
