using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(AbilityTemplate))]
[RequireComponent(typeof(NetworkView))]

public class AbilityPickup : MonoBehaviour
{
    Transform player;
    private bool isActive = false;
    public AbilityTemplate ability;
    bool isColliding = false;

    void OnTriggerStay2D(Collider2D collider)
    {
        //Check if activate button was pressed last frame
        //Aka. does the player want to pickup the item, "A" get an other function
        if (collider.gameObject.tag == "Will" || collider.gameObject.tag == "Deceit")
        {
            player = collider.transform;
            isColliding = true;
        }
        else
        {
            player = null;
        }

    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (isActive)
        {
            player.parent.FindChild("PickUpDisplay").gameObject.SetActive(false);
            isActive = false;
            player.GetComponent<Keybinds>().isPickingUpItemOrAbility = false;
        }
        isColliding = false;
    }

    void Update()
    {
        if (isActive && isColliding)
        {
            Pickup();
        }
        else if (isColliding)
        {
            //Check if pickup button is pressed
            if (Input.GetButtonDown("A"))
            {
                isActive = true;
                player.parent.FindChild("PickUpDisplay").GetComponent<FollowGameObject>().UpdatePos();
                player.parent.FindChild("PickUpDisplay").gameObject.SetActive(true);
            }
        }
    }
    public void Pickup()
    {
        // Player is picking up ability
        player.GetComponent<Keybinds>().isPickingUpItemOrAbility = true;

        //If A is chosen
        if (Input.GetButtonDown("A"))
        {
            if (Network.peerType != NetworkPeerType.Disconnected)
            {
                this.GetComponent<NetworkView>().RPC("disableAbility", RPCMode.All);
            }
            else
            {
                //gameObject.SetActive(false);
                GetComponent<Collider2D>().enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Animator>().enabled = false;
                GetComponent<ItemPickup>().enabled = false;
            }

            player.parent.FindChild("PickUpDisplay").gameObject.SetActive(false);
            player.GetComponent<Keybinds>().isPickingUpItemOrAbility = false;
            player.GetComponent<ItemAbilityManager>().pickUpCooldown();
        }

        //If X is chosen
        if (Input.GetButtonDown("X"))
        {
            if (Network.peerType != NetworkPeerType.Disconnected)
            {
                this.GetComponent<NetworkView>().RPC("addAbility", RPCMode.All, "X", player.tag);
                this.GetComponent<NetworkView>().RPC("disableAbility", RPCMode.All);
            }
            else
            {
                player.GetComponent<ItemAbilityManager>().addAbility("X", ability);
                //gameObject.SetActive(false);
                GetComponent<Collider2D>().enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Animator>().enabled = false;
                GetComponent<ItemPickup>().enabled = false;
            }

            player.parent.FindChild("PickUpDisplay").gameObject.SetActive(false);
            player.GetComponent<Keybinds>().isPickingUpItemOrAbility = false;
            player.GetComponent<ItemAbilityManager>().pickUpCooldown();
        }

        //If Y is chosen
        if (Input.GetButtonDown("Y"))
        {
            if (Network.peerType != NetworkPeerType.Disconnected)
            {
                this.GetComponent<NetworkView>().RPC("addAbility", RPCMode.All, "Y", player.tag);
                this.GetComponent<NetworkView>().RPC("disableAbility", RPCMode.All);
            }
            else
            {
                player.GetComponent<ItemAbilityManager>().addAbility("Y", ability);
                //gameObject.SetActive(false);
                GetComponent<Collider2D>().enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Animator>().enabled = false;
                GetComponent<ItemPickup>().enabled = false;
            }

            player.parent.FindChild("PickUpDisplay").gameObject.SetActive(false);
            player.GetComponent<Keybinds>().isPickingUpItemOrAbility = false;
            player.GetComponent<ItemAbilityManager>().pickUpCooldown();
        }

        //If B is chosen
        if (Input.GetButtonDown("B"))
        {
            if (Network.peerType != NetworkPeerType.Disconnected)
            {
                this.GetComponent<NetworkView>().RPC("addAbility", RPCMode.All, "B", player.tag);
                this.GetComponent<NetworkView>().RPC("disableAbility", RPCMode.All);
            }
            else
            {
                player.GetComponent<ItemAbilityManager>().addAbility("B", ability);
                //gameObject.SetActive(false);
                GetComponent<Collider2D>().enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Animator>().enabled = false;
                GetComponent<ItemPickup>().enabled = false;
            }

            player.parent.FindChild("PickUpDisplay").gameObject.SetActive(false);
            player.GetComponent<Keybinds>().isPickingUpItemOrAbility = false;
            player.GetComponent<ItemAbilityManager>().pickUpCooldown();
        }
    }
    [RPC]
    void disableAbility()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Animator>().enabled = false;
        GetComponent<ItemPickup>().enabled = false;
    }

    [RPC]
    void addAbility(string _key, string _tag)
    {
        Transform pickupPlayer;
        pickupPlayer = GameObject.FindWithTag(_tag).transform;
        pickupPlayer.GetComponent<ItemAbilityManager>().addAbility(_key, ability);
    }
}