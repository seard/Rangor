using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]

public class AnimationController : MonoBehaviour
{
    public Vector2 playerDirection;

    // Default variables
    PlayerController controller;
    Animator animator;

    static class AnimatorState
    {
        public static int Run_Up = Animator.StringToHash("Base.Run_Up");
        public static int Run_Down = Animator.StringToHash("Base.Run_Down");
        public static int Run_Right = Animator.StringToHash("Base.Run_Right");

        public static int Idle_Up = Animator.StringToHash("Base.Idle_Up");
        public static int Idle_Down = Animator.StringToHash("Base.Idle_Down");
        public static int Idle_Right = Animator.StringToHash("Base.Idle_Right");

        public static int Dash_Up = Animator.StringToHash("Base.Dash_Up");
        public static int Dash_Down = Animator.StringToHash("Base.Dash_Down");
        public static int Dash_Right = Animator.StringToHash("Base.Dash_Right");

        public static int Run_Grab_Up = Animator.StringToHash("Base.Run_Grab_Up");
        public static int Run_Grab_Down = Animator.StringToHash("Base.Run_Grab_Down");
        public static int Run_Grab_Right = Animator.StringToHash("Base.Run_Grab_Right");

        public static int Grab_Up = Animator.StringToHash("Base.Grab_Up");
        public static int Grab_Down = Animator.StringToHash("Base.Grab_Down");
        public static int Grab_Right = Animator.StringToHash("Base.Grab_Right");

        public static int Pickup_Down = Animator.StringToHash("Base.Pickup_Down");

        public static int Counter = Animator.StringToHash("Base.Counter");
    }

    // Use this for initialization
    void Start()
    {
        // Initialize components
        animator = GetComponent<Animator>();
        // Playercontroller can be on self or parent, check both
        if (transform.parent)
            if (!(controller = transform.parent.GetComponent<PlayerController>()))
                controller = transform.GetComponent<PlayerController>();

        if (!controller && tag == "Will" || tag == "Deceit")
            Debug.Log("Controller missing");
    }

    // Update is called once per frame
    void Update()
    {

        if (controller)
            playerDirection = controller.GetDirection().normalized;

        // Set animator values
        // Abs on dirVector.x for mirroring animation
        animator.SetFloat("SpeedX", Mathf.Abs(playerDirection.x));
        animator.SetFloat("SpeedY", playerDirection.y);

        if (controller)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.nameHash == AnimatorState.Run_Up)
                animator.speed = Mathf.Abs(controller.GetDirection().y) * (controller.speed / 150);
            else if(stateInfo.nameHash == AnimatorState.Run_Down)
                animator.speed = Mathf.Abs(controller.GetDirection().y) * (controller.speed / 150);
            else if (stateInfo.nameHash == AnimatorState.Run_Right)
                animator.speed = Mathf.Abs(controller.GetDirection().x) * (controller.speed / 150);
            else
                animator.speed = 1.0f;
        }
        else
        {
            animator.speed = 1.0f;
        }
    }

    void teleportingFalse()
    {
        animator.SetBool("Teleporting", false);
    }

    void counterFalse()
    {
        animator.SetBool("Countering", false);
    }

    void lockControls()
    {
        controller.lockControls = true;
    }

    void unlockControls()
    {
        controller.lockControls = false;
    }

    void pickupFalse()
    {
        animator.SetBool("Pickup", false);
    }

    void grabbingFalse()
    {
        animator.SetBool("Grabbing", false);
    }

    void deactivateIndicators()
    {
        TargetIndicatorController.DeactivateAOECircleIndicator();
        TargetIndicatorController.DeactivateFrontCircleIndicator();
        TargetIndicatorController.DeactivateFrontConeIndicator();
        TargetIndicatorController.DeactivateFrontLineIndicator();
    }

    void DoDTeleport()
    {
        animator.SetBool("Teleporting", false);
        transform.parent.GetComponentInParent<ItemAbilityManager>().items[GetComponentInParent<NetAnimator>().tempdodkey].triggerTeleport();
    }
}