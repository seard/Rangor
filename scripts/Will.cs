using UnityEngine;
using System.Collections;

public class Will : MonoBehaviour
{
    //Run when object is created
    void Awake()
    {
        Initiate();
    }

    void Initiate()
    {
        if (Network.peerType != NetworkPeerType.Disconnected)
        {
            if (!transform.FindChild("Player").GetComponent<NetworkView>().isMine) // Execute if player is not mine
            {
                transform.FindChild("GameCamera").gameObject.SetActive(false);
                transform.FindChild("Player").FindChild("Observer").gameObject.SetActive(false);
                transform.FindChild("Player").GetComponentInChildren<PlayerController>().isNetwork = true;
                transform.FindChild("Player").GetComponentInChildren<Keybinds>().enabled = false;
                transform.FindChild("PickUpDisplay").gameObject.SetActive(false);
                transform.FindChild("Player").GetComponentInChildren<WillOverTime>().enabled = false;
                transform.FindChild("Player").GetComponentInChildren<Dash>().enabled = false;
                Destroy(transform.FindChild("Player").GetComponentInChildren<Grab>());
                Destroy(transform.FindChild("Player").FindChild("PlayerGraphics").FindChild("TargetIndicators").gameObject);
            }
            else // Execute if player is mine
            {
                transform.FindChild("Player").GetComponentInChildren<ItemAbilityManager>().enabled = true;
            }
        }

        transform.FindChild("Player").GetComponentInChildren<ItemAbilityManager>().willAddAbilitiesAndItems();
    }
}
