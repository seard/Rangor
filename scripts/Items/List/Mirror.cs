using UnityEngine;
using System.Collections;

public class Mirror : ItemTemplate
{
    // Declare itemspecific variables here:
    public Transform MirrorDeceit;
    public Transform MirrorWill;

    public override void ItemStart()
    {
        // Trigger cooldown, set active and make unavailable.
        cooldownTimer = cooldownTime;
        available = false;
        activeTimer = activeTime;

        if (!MirrorDeceit)
            Debug.LogError("MirrorDeceitPrefab missing");
        if (!MirrorWill)
            Debug.LogError("MirrorWillPrefab missing");

        // On button press here:
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            if(thisPlayer.tag == "Deceit")
                GameObject.Instantiate(MirrorDeceit, thisPlayer.transform.position, thisPlayer.transform.rotation);
            if (thisPlayer.tag == "Will")
                GameObject.Instantiate(MirrorWill, thisPlayer.transform.position, thisPlayer.transform.rotation);
        }
        else
        {
            if (thisPlayer.tag == "Deceit")
                Network.Instantiate(MirrorDeceit, thisPlayer.transform.position, thisPlayer.transform.rotation, 0);
            if (thisPlayer.tag == "Will")
                Network.Instantiate(MirrorWill, thisPlayer.transform.position, thisPlayer.transform.rotation, 0);
        }
    }

    public override void ItemUpdate()
    {
        // Itemeffect loop here:
    }

    public override void ItemRelease()
    {
        // On button release effect here:
    }

    public override void EndCall()
    {
        // Itemeffect loop finished here:
    }

    public override void CoolDownEndCall()
    {
        // Item cooldown finished here:
    }

    public override void FixedItemBackground()
    {
        // Loop that runs even if item is on cooldown and not available:
    }

    public override void UpdateItemBackground()
    {
        // Loop that runs even if item is on cooldown and not available:
    }
}
