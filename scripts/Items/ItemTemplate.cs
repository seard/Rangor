using UnityEngine;
using System.Collections;

public class ItemTemplate : MonoBehaviour
{
    // Default variables
    public string currentKey;
    public int charges = 5;
    public bool pickupCooldown = false;
    public float cooldownTime = 2.0f;
    public float activeTime = 2.0f;
    public float cooldownTimer = 2.0f;
    public float activeTimer = 0.0f;
    public bool available = false;
    public Vector3 targetPos;
    public PlayerController controller;
    public Animator animator;
    public Transform thisPlayer;
    public Transform thatPlayer;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        UpdateItemBackground();
    }

    // Update is called twice per frame
    // Must be public! Used in ItemAbilityManager.cs
    public void FixedUpdate()
    {
        CooldownUpdate();
        CallUpdate();
        FixedItemBackground();
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

                --charges;
                // Remove item from inventory
                if (charges <= 0)
                {
                    thisPlayer.GetComponent<ItemAbilityManager>().dropItem(currentKey);
                }
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
                ItemUpdate();

                // Counts down to stop spell
                activeTimer -= Time.fixedDeltaTime;
            }
            // Reset variables if counter <= 0
            else if (activeTimer <= 0)
            {
                // Deactivation
                activeTimer = 0;
                if (!pickupCooldown)
                {
                    EndCall();
                }
            }
        }
    }
    // Call spell activation
    public void PrimaryCall()
    {
        if (available)
        {
            ItemStart();
        }
    }
    // Should be called on button release
    public void SecondaryCall()
    {
        if (!pickupCooldown)
        {
            ItemRelease();
        }
    }

    // Called on button press
    public virtual void ItemStart()
    {

    }

    // Called when spell is active
    public virtual void ItemUpdate()
    {

    }

    // Called on button release
    public virtual void ItemRelease()
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
    public virtual void FixedItemBackground()
    {

    }

    // Allways running in background
    public virtual void UpdateItemBackground()
    {

    }

    // teleportfunction that dod uses. It's ok to override in other items cause inheritance
    public virtual void triggerTeleport()
    {

    }
}