using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NetworkView))]

public class NetMirror : MonoBehaviour
{
    private Vector3 netRealPosition;
    private Vector3 netDirVector;
    private PlayerController controller;
    private MirrorKeybinds keybinds;

    GameObject observer;

    // Use this for initialization
    void Start()
    {
        if (!observer)
            observer = transform.FindChild("Player").FindChild("Observer").gameObject;

        if(tag == "MirrorWill" && GetNetPlayer.GetThisPlayer().tag == "Will")
            observer.SetActive(true);
        else if (tag == "MirrorDeceit" && GetNetPlayer.GetThisPlayer().tag == "Deceit")
            observer.SetActive(true);
        else
            observer.SetActive(false);
    }

    void Awake()
    {
        controller = transform.GetComponentInChildren<PlayerController>();
        keybinds = transform.GetComponentInChildren<MirrorKeybinds>();
        if (GetComponent<NetworkView>().isMine)
        {
            keybinds.enabled = true;
            controller.enabled = true;
            controller.isNetwork = false;
        }
    }

    void FixedUpdate()
    {
        if (!GetComponent<NetworkView>().isMine)
        {
            float distance = Vector3.Distance(transform.position, netRealPosition);
            if (distance >= 2)
            {
                transform.position = netRealPosition;
            }
            else if (distance > 0.5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, netRealPosition, Time.deltaTime * 5);
            }
        }
    }

    // Network communication
    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        if (GetComponent<NetworkView>().isMine)
        {
            if (stream.isWriting) //Owner sends info
            {
                Vector3 position = transform.FindChild("Player").transform.position;
                Vector3 dirVector = controller.GetDirection();
                stream.Serialize(ref position);
                stream.Serialize(ref dirVector);
            }
        }
        else if (!GetComponent<NetworkView>().isMine && Network.connections.Length > 0)
        {
            //None owner receive info
            Vector3 position = Vector3.zero;
            Vector3 dirVector = Vector3.zero;
            stream.Serialize(ref position);
            stream.Serialize(ref dirVector);
            netRealPosition = position;
            netDirVector = dirVector;

            controller.SetDirection(netDirVector);
            controller.transform.position = netRealPosition;
        }
    }

    [RPC]
    void teleport2(Vector3 newPosition)
    {
        transform.position = newPosition;
        netRealPosition = newPosition;
    }

}