using UnityEngine;
using System.Collections;

public class Deceit : MonoBehaviour 
{
    Transform Will;

    IEnumerator clientDeceitAwake()
    {
        yield return new WaitForEndOfFrame();
        transform.FindChild("Player").GetComponentInChildren<ItemAbilityManager>().deceitAddAbilitiesAndItems();
        Will = GameObject.FindWithTag("Will").transform;
    }

    //Run when object is created
    void Awake()
    {
        if (Network.peerType != NetworkPeerType.Disconnected)
        {
            if (!transform.FindChild("Player").GetComponent<NetworkView>().isMine) // Execute if player is not mine
            {
                Destroy(transform.FindChild("GameCamera").gameObject);
                Destroy(transform.FindChild("Player").GetComponentInChildren<Keybinds>());
                Destroy(transform.FindChild("PickUpDisplay").gameObject);
                Destroy(transform.FindChild("Player").FindChild("PlayerGraphics").FindChild("TargetIndicators").gameObject);

                transform.FindChild("Player").FindChild("Observer").gameObject.SetActive(false);
                transform.FindChild("Player").GetComponentInChildren<PlayerController>().isNetwork = true;
                transform.FindChild("Player").GetComponentInChildren<Dash>().enabled = false;
                Destroy(transform.FindChild("Player").GetComponentInChildren<Grab>());

                GameObject.Find("Will(Clone)").transform.FindChild("Player").GetComponent<ItemAbilityManager>().deceitConnectAddThatPlayer();
            }
            else // Execute if player is mine
            {
                transform.gameObject.SetActive(true);
                transform.FindChild("Player").GetComponentInChildren<ItemAbilityManager>().enabled = true;
            }
        }
        StartCoroutine(clientDeceitAwake());
    }
}
