using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NetworkView))]

public class NetWill : MonoBehaviour
{
    private Vector2 netRealPosition;
    private Vector2 netDirVector;
    private Vector2 netAdditiveDirVector;
    private float netSpeed;

    private PlayerController controller;

    // Use this for initialization
    void Start()
    {
        netRealPosition = transform.position;

        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<NetworkView>().isMine)
        {
            float distance = Vector3.Distance(transform.position, netRealPosition);
            if (distance >= 2)
            {
                transform.position = netRealPosition;
            }
        }
    }

    Vector3 dir;
    void FixedUpdate()
    {
        dir = controller.GetDirection3();
    }

    // Network communication
    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        if (GetComponent<NetworkView>().isMine)
        {
            if (stream.isWriting) //Owner sends info
            {
                Vector3 position = transform.position;
                Vector3 dirVector = dir;
                Vector3 additiveDirVector = controller.GetAdditiveDirVector();
                float speed = controller.speed;

                stream.Serialize(ref position);
                stream.Serialize(ref dirVector);
                stream.Serialize(ref additiveDirVector);
                stream.Serialize(ref speed);
            }
        }
        else if (!GetComponent<NetworkView>().isMine && Network.connections.Length > 0)
        {
            Vector3 position = Vector3.zero;
            Vector3 dirVector = Vector3.zero;
            Vector3 additiveDirVector = Vector3.zero;
            float speed = 0;

            stream.Serialize(ref position);
            stream.Serialize(ref dirVector);
            stream.Serialize(ref additiveDirVector);
            stream.Serialize(ref speed);

            netRealPosition = position;
            netDirVector = dirVector;
            netAdditiveDirVector = additiveDirVector;
            netSpeed = speed;

            if (controller)
            {
                controller.SetDirection(netDirVector);
                controller.SetAdditiveDirVector(additiveDirVector);
                transform.position = position;
                controller.speed = netSpeed;
            }
        }
    }

    [RPC]
    void teleportWill(Vector3 newPosition)
    {
        transform.position = newPosition;
        netRealPosition = newPosition;
    }
}