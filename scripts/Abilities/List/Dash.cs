using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class Dash : AbilityTemplate
{
    // Declare spellspecific variables here:
    Vector2 dashDirection;
    public float dashPower = 3.5f;
    public AudioClip dashSound;

    public float blockDamage;
    public GameObject debris;
    public Texture2D dissolveTex;

    public override void SpellStart()
    {
        // Trigger cooldown, set active and make unavailable.
        // If block in the way, don't perform dash
        if (!controller.RayCast(controller.FacingDirection(), 0.75f) && controller.GetDirection().magnitude > 0)
        {
            cooldownTimer = cooldownTime;
            available = false;
            activeTimer = activeTime;
            animator.SetBool("Dashing", true);

            GetComponent<AudioSource>().PlayOneShot(dashSound);

            thisPlayer.GetComponentInChildren<NetworkView>().RPC(("dashTrue"), RPCMode.All);

            TargetIndicatorController.FrontLineIndicator(2.0f);
        }

        
        // On button press here:
    }

    public override void SpellUpdate()
    {
        // Spell loop here:
        //GameObject o;

        // If dashing into wall at 45 degree steep angle
        if (Vector2.Distance(-controller.RayCastHit(controller.GetDirection()).normal, controller.GetDirection()) <= 0.293f)
        {
            GetComponent<NetworkView>().RPC("RayCastBlockBreak", RPCMode.All, (Vector3)GetComponent<Collider2D>().bounds.center, (Vector3)controller.GetDirection());
        }
        
        // Call to move
        controller.ForceMove(controller.GetDirection() * dashPower, 0, true, true);
    }

    public override void SpellRelease()
    {
        // On button release effect here:
    }

    public override void CoolDownEndCall()
    {
        // Spell loop finished here:
    }

    // Called when spell has finished
    public override void EndCall()
    {
        if(Network.peerType != NetworkPeerType.Disconnected)
            thisPlayer.GetComponentInChildren<NetworkView>().RPC(("dashFalse"), RPCMode.All);
        controller.lockControls = false;
    }

    [RPC]
    void blockBreak()
    {

    }

    [RPC]
    void dashTrue()
    {
        animator.SetBool("Dashing", true);
    }

    [RPC]
    void dashFalse()
    {
        animator.SetBool("Dashing", false);
    }

    [RPC]
    void RayCastBlockBreak(Vector3 _position, Vector3 _direction)
    {
        LayerMask collidables = controller.collidable;

        GameObject o;

        //if (o = Physics2D.Raycast(_position, _direction).transform.gameObject)
        if(o = controller.RayCastObject(controller.GetDirection()))
        {
            if(o.layer == LayerMask.NameToLayer("Environment"))
            {
                if (!o.GetComponent<BlockBreak>())
                {
                    o.AddComponent<BlockBreak>();
                    o.GetComponent<BlockBreak>().debris = debris;
                    o.GetComponent<BlockBreak>().dissolveTexture = dissolveTex;
                    o.GetComponent<BlockBreak>().Initiate();
                }

                o.GetComponent<BlockBreak>().Damage(blockDamage);
            }

            CameraShake.Shake(0.5f);
            activeTimer = 0;
        }
    }
}