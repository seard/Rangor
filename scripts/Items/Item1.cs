using UnityEngine;
using System.Collections;
public class Item1 : ItemTemplate
{
    // Declare itemspecific variables here:
    public override void ItemStart()
    {
        // Trigger cooldown, set active and make unavailable.
        cooldownTimer = cooldownTime;
        available = false;
        activeTimer = activeTime;

        // On button press here:
        Debug.Log("Item push button");

    }

    public override void ItemUpdate()
    {
        // Itemeffect loop here:
        Debug.Log("Item loop");
    }

    public override void ItemRelease()
    {
        // On button release effect here:
        Debug.Log("Item release");
    }

    public override void EndCall()
    {
        // Itemeffect loop finished here:
        Debug.Log("Item end");
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