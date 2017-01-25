using UnityEngine;
using System.Collections;
public class DiamondOfDisplacement : ItemTemplate
{
    // Declare itemspecific variables here:
    public float teleportDistance;
    private bool isActive = false;
    public LayerMask targetCollidable;
    private Vector3 newTargetPos;

    public override void ItemUpdate()
    {
        // Itemeffect loop here:
    }

    public override void ItemRelease()
    {
        if (isActive)
        {
            // On button release effect here:
            newTargetPos = getNearestFreeSpot(thisPlayer.GetComponent<CircleCollider2D>().bounds.center + targetPos);
            isActive = false;

            // Start teleport animation
            thisPlayer.GetComponent<NetworkView>().RPC("setDoDAnimationBool", RPCMode.All, thisPlayer.tag, currentKey, true);

            // Hide target circle
            thisPlayer.FindChild("PlayerGraphics").FindChild("TargetIndicator").gameObject.SetActive(false);

            // Trigger cooldown, set active and make unavailable.
            cooldownTimer = cooldownTime;
            available = false;
            activeTimer = activeTime;
        }
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
        if (isActive)
        {
            TargetIndicatorController.FrontLineIndicator(teleportDistance, false);

            targetPos = controller.FacingDirection3() * teleportDistance;
        }
    }

    public override void UpdateItemBackground()
    {
        // Loop that runs even if item is on cooldown and not available:
    }

    public bool checkCollision(Vector2 _collisionPos, float _rayLenght)
    {
        return Physics2D.Raycast(_collisionPos, Vector2.up + Vector2.right, _rayLenght, targetCollidable);
    }

    public Vector3 getNearestFreeSpot(Vector3 _collisionPos)
    {
        int loops = 0;
        CircleCollider2D collider = thisPlayer.GetComponent<CircleCollider2D>();
        GameObject map = GameObject.FindGameObjectWithTag("EnvironmentLoader");
        Vector3 testPos = _collisionPos;
        bool isColliding = true;
        float angle = 0.0f;
        float radius = 0.0f;

        // Look for free position in a rotating circle motion from player moving outwards
        while (isColliding && loops < 500)
        {
            loops++;
            testPos.x = Mathf.Cos(angle) * radius + _collisionPos.x;
            testPos.y = Mathf.Sin(angle) * radius + _collisionPos.y;

            //check if inside map
            if (testPos.x > 0 && testPos.x < map.GetComponent<LoadEnvironment>().mapSize.x && testPos.y > 0 && testPos.y < map.GetComponent<LoadEnvironment>().mapSize.y)
            {
                // check for collision
                if (!checkCollision(testPos - new Vector3((collider.radius / 2), 0), collider.radius))
                {
                    isColliding = false;
                }
            }
            angle += 15;
            // One circle test complete, increase radius
            if (angle % 360 == 0)
            {
                radius += 0.1f;
                angle = 0;
            }
        }
        // Free position found, return that position
        return testPos;
    }

    public override void triggerTeleport()
    {
        // Teleport
        thisPlayer.GetComponent<NetworkView>().RPC("doDTeleport", RPCMode.All, thisPlayer.tag, currentKey, newTargetPos);
        thisPlayer.transform.position = newTargetPos;
    }
}
