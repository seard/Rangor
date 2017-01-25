using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WillOverTime : AbilityTemplate
{
    // Declare spellspecific variables here:
    private Queue<Vector3> willPositions = new Queue<Vector3>();
    private Queue<Vector3> deceitPositions = new Queue<Vector3>();
    private Queue<Vector3> willDirs = new Queue<Vector3>();
    private Queue<Vector3> deceitDirs = new Queue<Vector3>();
    private GameObject deceitShadow;
    private GameObject willShadow;
    public float stepTimer;
    private Vector3 willShadowPos;
    private Vector3 deceitShadowPos;
    private Vector3 willShadowDir;
    private Vector3 deceitShadowDir;
    private bool activeAbility = false;
    private bool shadowsAlive = false;
    public float Speed = 4.0f;
    public int saveSteps = 10;
    public float saveSeconds = 2.0f;
    public GameObject prefabShadowP1;
    public GameObject prefabShadowP2;

    public override void SpellStart()
    {
        // On button press here:
        activeAbility = true;
    }

    public override void SpellUpdate()
    {
        // Spell loop here:
    }

    public override void SpellRelease()
    {
        // On button release effect here:
        //Debug.Log("ButtonRelease");
        if (available && shadowsAlive)
        {
            activeAbility = false;
            teleport();
        }
    }

    public override void CoolDownEndCall()
    {
        // Spell loop finished here:
    }

    public override void FixedSpellBackground()
    {
        Speed = thisPlayer.GetComponent<PlayerController>().speed / 45.0f;

        //update stepTimer
        stepTimer += Time.fixedDeltaTime;

        // If multiplayer
        if (Network.connections.Length > 0)
        {

            // Check if a step should be triggered
            if (stepTimer > saveSeconds / saveSteps)
            {
                // Add positions into que
                if(thisPlayer)
                    willPositions.Enqueue(thisPlayer.position);
                if(thatPlayer)
                    deceitPositions.Enqueue(thatPlayer.position);

                // Add dirVector into que
                willDirs.Enqueue(thisPlayer.GetComponent<PlayerController>().GetDirection());
                deceitDirs.Enqueue(thisPlayer.GetComponent<PlayerController>().GetDirection());

                // Are there maximum amount of steps saved
                if (willPositions.Count >= saveSteps)
                {
                    // Set willShadow position and remove pos from que
                    willShadowPos = willPositions.Dequeue();
                    willShadowDir = willDirs.Dequeue();
                }
                else
                {
                    // Set willShadow position without removing it from que
                    willShadowPos = willPositions.Peek();
                    willShadowDir = willDirs.Peek();
                }

                // Are there maxium amount of steps saved
                if (deceitPositions.Count >= saveSteps)
                {
                    // Set deceitShadowPos position and remove pos from que
                    deceitShadowPos = deceitPositions.Dequeue();
                    deceitShadowDir = deceitDirs.Dequeue();
                }
                else
                {
                    // Set deceitShadowPos position without removing it from que
                    deceitShadowPos = deceitPositions.Peek();
                    deceitShadowDir = deceitDirs.Peek();
                }

                //Reset timer
                stepTimer = 0.0f;
            }

            if (activeAbility)
            {
                if (!shadowsAlive)
                {
                    // Spawn shadows
                    willShadow = GameObject.Instantiate(prefabShadowP1, willShadowPos, Quaternion.identity) as GameObject;
                    deceitShadow = GameObject.Instantiate(prefabShadowP2, deceitShadowPos, Quaternion.identity) as GameObject;
                    shadowsAlive = true;
                }
                else
                {
                    // Move shadows to next position
                    willShadow.transform.position = Vector3.MoveTowards(willShadow.transform.position, willShadowPos, Time.fixedDeltaTime * Speed);
                    willShadow.GetComponent<AnimationController>().playerDirection = willShadowDir;
                    deceitShadow.transform.position = Vector3.MoveTowards(deceitShadow.transform.position, deceitShadowPos, Time.fixedDeltaTime * Speed);
                    deceitShadow.GetComponent<AnimationController>().playerDirection = deceitShadowDir;
                }
            }

            if (shadowsAlive && !activeAbility) // If ability is inactive and shadows are alive
            {
                Destroy(willShadow);
                Destroy(deceitShadow);
                shadowsAlive = false;
            }
        }
        else // Single player
        {
            // Check if a step should be triggered
            if (stepTimer > saveSeconds / saveSteps)
            {
                // Add position into queue
                willPositions.Enqueue(thisPlayer.position);
                willDirs.Enqueue(thisPlayer.GetComponent<PlayerController>().GetDirection());

                // Are there maximum amount of steps saved
                if (willPositions.Count >= saveSteps)
                {
                    // Set willShadow position and remove pos from que
                    willShadowPos = willPositions.Dequeue();
                    willShadowDir = willDirs.Dequeue();
                }
                else
                {
                    // Set willShadow position without removing it from que
                    willShadowPos = willPositions.Peek();
                    willShadowDir = willDirs.Peek();
                }
                //Reset timer
                stepTimer = 0.0f;
            }
            if (activeAbility)
            {
                if (!shadowsAlive)
                {
                    // Spawn shadow
                    willShadow = GameObject.Instantiate(prefabShadowP1, willShadowPos, Quaternion.identity) as GameObject;
                    shadowsAlive = true;
                }
                else
                {
                    // Move shadow to next position
                    willShadow.transform.position = Vector3.MoveTowards(willShadow.transform.position, willShadowPos, Time.fixedDeltaTime * Speed);
                    willShadow.GetComponent<AnimationController>().playerDirection = willShadowDir;
                    if(willShadowDir.x != 0)
                        willShadow.transform.localScale = new Vector3(Mathf.Sign(willShadowDir.x), 1, 1);
                }
            }

            if (shadowsAlive && !activeAbility) // If ability is inactive and shadows are alive
            {
                Destroy(willShadow);
                shadowsAlive = false;
            }
        }
    }

    public override void UpdateSpellBackground()
    {
        // Loop that runs even if spell is on cooldown and not available:
    }

    // Teleport players
    private void teleport()
    {
        if (Network.connections.Length > 0)
        {
            thisPlayer.GetComponentInChildren<NetworkView>().RPC(("teleportWill"), RPCMode.All, willShadowPos);
            thatPlayer.GetComponentInChildren<NetworkView>().RPC(("teleportDeceit"), RPCMode.All, deceitShadowPos);
        }
        else
        {
            thisPlayer.transform.position = willShadowPos;
        }
        activeAbility = false;

        // Trigger cooldown, set active and make unavailable.
        cooldownTimer = cooldownTime;
        available = false;
        activeTimer = activeTime;
    }
}