using UnityEngine;
using System.Collections;
public class Tag : AbilityTemplate
{
    // Declare spellspecific variables here:
    public GameObject tagObject;

    public override void SpellStart()
    {
        // Trigger cooldown, set active and make unavailable.
        cooldownTimer = cooldownTime;
        available = false;
        activeTimer = activeTime;

        // On button press here:
        if (TargetIndicatorController.AOECircleIndicator(3.0f))
        {
            GameObject o = Network.Instantiate(tagObject, thatPlayer.position, Quaternion.identity, 0) as GameObject;
            o.GetComponent<TagEntity>().target = thatPlayer;
        }
    }

    public override void SpellUpdate()
    {
        // Spell loop here:
    }

    public override void SpellRelease()
    {
        // On button release effect here:
    }

    // Called when spell has finished
    public override void EndCall()
    {
        // Spell loop finished here:
    }

    // Called when cooldown has finished

    public override void CoolDownEndCall()
    {
        // Spell loop finished here:
    }

    public override void FixedSpellBackground()
    {
        // Loop that runs even if spell is on cooldown and not available:
    }

    public override void UpdateSpellBackground()
    {
        // Loop that runs even if spell is on cooldown and not available:
    }
}