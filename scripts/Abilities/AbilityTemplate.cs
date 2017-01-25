using UnityEngine;
using System.Collections;

public class AbilityTemplate : MonoBehaviour
{
    // Default variables
    public bool pickupCooldown = false;
    public float cooldownTime = 2.0f;
    public float activeTime = 2.0f;
    public float cooldownTimer = 2.0f;
    public float activeTimer = 0.0f;
    public bool available = false;
    public PlayerController controller;
    public Animator animator;
    public Transform thisPlayer;
    public Transform thatPlayer;

    // Spell-specific variables
    // Use this for initialization
    public virtual void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        UpdateSpellBackground();
    }

    // Update is called twice per frame
    // Must be public! Used in ItemAbilityManager.cs
    public void FixedUpdate()
    {
        CooldownUpdate();
        CallUpdate();
        FixedSpellBackground();
    }
    public void CooldownUpdate()
    {
        // Cooldown update
        if (!available && cooldownTimer <= 0)
        {
            // End call
            cooldownTimer = 0;
            available = true;
            // No EndCall() if player picked up spell
            if (!pickupCooldown)
            {
                CoolDownEndCall();
            }
            pickupCooldown = false;
        }
        else if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.fixedDeltaTime;
        }
    }
    // Updates cooldown and ability call
    public void CallUpdate()
    {

        // If currently being used
        if (!available)
        {
            // Ability call
            if (activeTimer > 0)
            {
                // If player stops moving for some reason, ex. hitting a wall, kill everything
                if (!controller.IsMoving())
                {
                    activeTimer = 0;
                }
                SpellUpdate();

                // Counts down to stop spell
                activeTimer -= Time.fixedDeltaTime;
            }
            // Reset variables if counter <= 0
            else if (activeTimer <= 0)
            {
                // Deactivation
                activeTimer = 0;
                EndCall();
            }
        }
    }
    // Call spell activation
    public void PrimaryCall()
    {
        if (available)
        {
            SpellStart();
        }
    }
    // Should be called on button release
    public void SecondaryCall()
    {
        if (!pickupCooldown)
        {
            SpellRelease();
        }
    }

    // Called on button press

    public virtual void SpellStart()
    {

    }

    // Called when spell is active
    public virtual void SpellUpdate()
    {

    }

    // Called on button release
    public virtual void SpellRelease()
    {

    }

    // Called when spell has finished
    public virtual void EndCall()
    {

    }

    // Called when cooldown has finished
    public virtual void CoolDownEndCall()
    {

    }

    // Allways running in background
    public virtual void FixedSpellBackground()
    {

    }

    // Allways running in background
    public virtual void UpdateSpellBackground()
    {

    }
}