using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(ItemTemplate))]
[RequireComponent(typeof(NetworkView))]

public class ItemPickup : MonoBehaviour
{
    Transform player;
    private bool isActive = false;
    public ItemTemplate item;
    bool isColliding = false;

    void OnTriggerStay2D(Collider2D other)
    {
        //Check if activate button was pressed last frame
        //Aka. does the player want to pickup the item, "A" get an other function
        if (other.gameObject.tag == "Will" || other.gameObject.tag == "Deceit")
        {
            player = other.transform;
            isColliding = true;
        }
        else
        {
            player = null;
        }

    }

    void OnTriggerExit2D(Collider2D other)
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
                this.GetComponent<NetworkView>().RPC("disableItem", RPCMode.All);
            }
            else
            {
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
                this.GetComponent<NetworkView>().RPC("addItem", RPCMode.All, "X", player.tag);
                this.GetComponent<NetworkView>().RPC("disableItem", RPCMode.All);
            }
            else
            {
                player.GetComponent<ItemAbilityManager>().addItem("X", item);
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
                this.GetComponent<NetworkView>().RPC("addItem", RPCMode.All, "Y", player.tag);
                this.GetComponent<NetworkView>().RPC("disableItem", RPCMode.All);
            }
            else
            {
                player.GetComponent<ItemAbilityManager>().addItem("Y", item);
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
                this.GetComponent<NetworkView>().RPC("addItem", RPCMode.All, "B", player.tag);
                this.GetComponent<NetworkView>().RPC("disableItem", RPCMode.All);
            }
            else
            {
                player.GetComponent<ItemAbilityManager>().addItem("B", item);
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
    void disableItem()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Animator>().enabled = false;
        GetComponent<ItemPickup>().enabled = false;
    }

    [RPC]
    void addItem(string _key, string _tag)
    {
        GameObject.FindWithTag(_tag).transform.FindChild("PlayerGraphics").GetComponent<Animator>().SetBool("Pickup", true);

        Transform pickupPlayer;
        pickupPlayer = GameObject.FindWithTag(_tag).transform;
        pickupPlayer.GetComponent<ItemAbilityManager>().addItem(_key, item);
    }
}