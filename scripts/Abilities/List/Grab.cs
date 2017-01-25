
using UnityEngine;
using System.Collections;

public class Grab : MonoBehaviour
{
    public float distance;
    public float timeLimit;

    float grabTimer;

    public Transform thisPlayer;
    public Transform thatPlayer;

    void Start()
    {
        thisPlayer = GetNetPlayer.GetThisPlayer();
        thatPlayer = GetNetPlayer.GetThatPlayer();
    }

    void Update()
    {
        if(!thisPlayer)
            if (GetNetPlayer.GetThisPlayer())
                thisPlayer = GetNetPlayer.GetThisPlayer();
        if(!thatPlayer)
            if(GetNetPlayer.GetThatPlayer())
                thatPlayer = GetNetPlayer.GetThatPlayer();

        if (thisPlayer && thatPlayer)
        {
            if (Vector2.Distance(thisPlayer.position, thatPlayer.position) <= distance)
            {
                TargetIndicatorController.AOECircleIndicator(distance);

                GetComponent<NetworkView>().RPC("IsGrabbing", RPCMode.All);

                grabTimer += Time.deltaTime;

                if (grabTimer >= timeLimit)
                    GetComponent<NetworkView>().RPC("GameOver", RPCMode.All);
            }
            else if (grabTimer > 0)
            {
                GetComponent<NetworkView>().RPC("IsNotGrabbing", RPCMode.All);
                grabTimer -= Time.deltaTime;
            }
        }
    }
}