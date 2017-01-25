using UnityEngine;
using System.Collections;

public class GetNetPlayer : MonoBehaviour 
{
    static string thisTag = "";
    static Transform thisPlayer;
    static Transform thatPlayer;
    static Transform will;
    static Transform deceit;

    public Transform _thisPlayer;
    public Transform _thatPlayer;

    void Start()
    {
        NetUpdate();
    }

    void Update()
    {
        _thisPlayer = thisPlayer;
        _thatPlayer = thatPlayer;
    }

    public static void TrySetPlayers()
    {
        if (GameObject.FindGameObjectWithTag("Will"))
            will = GameObject.FindGameObjectWithTag("Will").transform;
        if (GameObject.FindGameObjectWithTag("Deceit"))
            deceit = GameObject.FindGameObjectWithTag("Deceit").transform;
    }

    public static void NetUpdate()
    {
        TrySetPlayers();

        if (will.GetComponent<NetworkView>().isMine)
        {
            thisPlayer = will;
            thatPlayer = deceit;
        }
        else
        {
            thisPlayer = deceit;
            thatPlayer = will;
        }
    }

    void SingleUpdate()
    {
        if (GameObject.FindWithTag("Will"))
            thatPlayer = thisPlayer = GameObject.FindWithTag("Will").transform;
        Debug.LogWarning("Setting thisPlayer and thatPlayer to same");
    }

    public static Transform GetThisPlayer()
    {
        if (!thisPlayer)
            NetUpdate();

        return thisPlayer;
    }

    public static Transform GetThatPlayer()
    {
        if (!thatPlayer)
            NetUpdate();

        return thatPlayer;
    }
}
