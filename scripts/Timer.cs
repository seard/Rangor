using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
    public float scoreTimer;
    public int showSeconds;
    public int showMinutes = 0;
    public int minutes;
    private bool counting = true;
    private bool showtime = false;

    Transform Will;
    Transform Deceit;

    // Use this for initialization
    void Start()
    {
        scoreTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Will)
            if (GameObject.FindGameObjectWithTag("Will"))
                Will = GameObject.FindGameObjectWithTag("Will").transform;
        if(!Deceit)
            if (GameObject.FindGameObjectWithTag("Deceit"))
                Deceit = GameObject.FindGameObjectWithTag("Deceit").transform;

        if (Will && Deceit)
        {
            if (Will.GetComponent<WinCondition>().gameOver)
            {
                counting = false;
                showtime = true;
            }
            if (counting)
            {
                scoreTimer += Time.deltaTime;
                showSeconds = (int)scoreTimer - 60 * showMinutes;
                if (showSeconds >= 60)
                {
                    showMinutes = showMinutes + 1;

                }
            }
        }
    }
    void OnGUI()
    {
        if (showtime)
        {
            if(showSeconds < 10)
                GUI.TextField(new Rect(Screen.width / 2 - 100, 10, 200, 20), showMinutes.ToString() + ":0" + showSeconds.ToString());
            else
                GUI.TextField(new Rect(Screen.width / 2 - 100, 10, 200, 20), showMinutes.ToString() + ":" + showSeconds.ToString());
        }
    }
}


