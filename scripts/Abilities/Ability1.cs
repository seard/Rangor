using UnityEngine;
using System.Collections;
public class Ability1 : AbilityTemplate
{
    // Declare spellspecific variables here:


    public override void SpellStart()
    {
        // Trigger cooldown, set active and make unavailable.
        cooldownTimer = cooldownTime;
        available = false;
        activeTimer = activeTime;

        // On button press here:
        Debug.Log("Ability push button");
    }

    public override void SpellUpdate()
    {
        // Spell loop here:
        Debug.Log("Ability loop");
    }

    public override void SpellRelease()
    {
        // On button release effect here:
        Debug.Log("Ability release");
    }

    // Called when spell has finished
    public override void EndCall()
    {
        // Spell loop finished here:
        Debug.Log("Ability end");
    }

    // Called when cooldown has finished

    public override void CoolDownEndCall()
    {
        // Spell loop finished here:
        Debug.Log("Ability end");
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