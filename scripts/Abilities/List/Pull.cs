﻿using UnityEngine;
using System.Collections;

public class Pull : AbilityTemplate
{
    // Declare spellspecific variables here:
    public GameObject pullEntity;
    public float pullDistance = 4.0f;

    public override void SpellStart()
    {
        // Trigger cooldown, set active and make unavailable.
        cooldownTimer = cooldownTime;
        available = false;
        activeTimer = activeTime;

        // On button press here:
        GameObject o;
        o = Network.Instantiate(pullEntity, GetComponent<Collider2D>().bounds.center, Quaternion.identity, 0) as GameObject;
        o.GetComponent<PullEntity>().targetTag = "Deceit";

        TargetIndicatorController.AOECircleIndicator(pullDistance);
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
