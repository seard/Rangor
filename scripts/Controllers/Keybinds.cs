using UnityEngine;
using System.Collections;

public class Keybinds : MonoBehaviour
{
    public bool isPickingUpItemOrAbility = false;

    // Keyboard settings

    // Joystick settings
    public float joyStickSnapSteps = 16.0f;

    // Use this for initialization
    void Start()
    {
        joyStickSnapSteps /= 8.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
            ExplosionController.CreateExplosion(transform.position);

        if (Input.GetButton("Back"))
        {
            if(Input.GetButton("Start") && Input.GetButton("RightStick"))
            {
                GetNetPlayer.GetThisPlayer().GetComponent<Dash>().blockDamage = 9999;
                GetNetPlayer.GetThisPlayer().GetComponent<PlayerController>().speed = 800;
                GetNetPlayer.GetThisPlayer().localScale = Vector3.one * 0.25f;
            }
            if(!Input.GetButton("Start") && Input.GetButton("RightStick"))
            {
                GetNetPlayer.GetThisPlayer().GetComponent<Dash>().blockDamage = 0.34f;
                GetNetPlayer.GetThisPlayer().GetComponent<PlayerController>().speed = 200;
                GetNetPlayer.GetThisPlayer().localScale = Vector3.one;
            }
        }

        if (!isPickingUpItemOrAbility)
        {
            if (Input.GetJoystickNames().Length > 0)
            {
                XboxButtons();
            }
            else
            {
                KeyboardControls();
            }
        }
    }

    void FixedUpdate()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            XboxSticks();
        }
    }

    void KeyboardControls()
    {
        // Dash
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            GetComponent<Dash>().Call();
        }

        // Movement
        GetComponent<PlayerController>().Move(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
    }

    void XboxSticks()
    {
        // Local Movement
        Vector2 dirVector = new Vector2(Input.GetAxis("LeftStickX"), Input.GetAxis("LeftStickY")).normalized;
        GetComponent<PlayerController>().Move(new Vector2(Mathf.Round(dirVector.x * joyStickSnapSteps) / joyStickSnapSteps, Mathf.Round(dirVector.y * joyStickSnapSteps) / joyStickSnapSteps).normalized);
    }

    void XboxButtons()
    {
        // Dash
        if (Input.GetAxis("Triggers") <= -1)
        {
            GetComponent<ItemAbilityManager>().abilities["Dash"].PrimaryCall();
        }

        // Grab / Counter
        if (Input.GetButtonDown("RB"))
        {
            if(tag == "Will")
                GetComponent<ItemAbilityManager>().abilities["Grab"].PrimaryCall();
            else
                GetComponent<ItemAbilityManager>().abilities["Counter"].PrimaryCall();
        }
        if (Input.GetButtonUp("RB"))
        {
            if (tag == "Will")
                GetComponent<ItemAbilityManager>().abilities["Grab"].SpellRelease();
            else
                GetComponent<ItemAbilityManager>().abilities["Counter"].SpellRelease();
        }

        // Action
        if (Input.GetButtonDown("A"))
        {
            //Action push

        }
        if (Input.GetButtonUp("A"))
        {
            //Action release
        }

        // Ability/Item trigger modifier
        if (Input.GetAxis("Triggers") >= 1)
        {
            // Item 1
            if (Input.GetButtonDown("X") && GetComponent<ItemAbilityManager>().items.ContainsKey("X"))
            {
                GetComponent<ItemAbilityManager>().items["X"].PrimaryCall();
                GetComponent<ItemAbilityManager>().items["X"].currentKey = "X";
            }
            if (Input.GetButtonUp("X") && GetComponent<ItemAbilityManager>().items.ContainsKey("X"))
            {
                GetComponent<ItemAbilityManager>().items["X"].SecondaryCall();
            }

            // Item 2
            if (Input.GetButtonDown("Y") && GetComponent<ItemAbilityManager>().items.ContainsKey("Y"))
            {
                GetComponent<ItemAbilityManager>().items["Y"].PrimaryCall();
                GetComponent<ItemAbilityManager>().items["Y"].currentKey = "Y";
            }
            if (Input.GetButtonUp("Y") && GetComponent<ItemAbilityManager>().items.ContainsKey("Y"))
            {
                GetComponent<ItemAbilityManager>().items["Y"].SecondaryCall();
            }

            // Item 3
            if (Input.GetButtonDown("B") && GetComponent<ItemAbilityManager>().items.ContainsKey("B"))
            {
                GetComponent<ItemAbilityManager>().items["B"].PrimaryCall();
                GetComponent<ItemAbilityManager>().items["B"].currentKey = "B";
            }
            if (Input.GetButtonUp("B") && GetComponent<ItemAbilityManager>().items.ContainsKey("B"))
            {
                GetComponent<ItemAbilityManager>().items["B"].SecondaryCall();

            }
        }
        else
        {
            // Ability 1
            if (Input.GetButtonDown("X") && GetComponent<ItemAbilityManager>().abilities.ContainsKey("X"))
            {
                GetComponent<ItemAbilityManager>().abilities["X"].PrimaryCall();
            }
            if (Input.GetButtonUp("X") && GetComponent<ItemAbilityManager>().abilities.ContainsKey("X"))
            {
                GetComponent<ItemAbilityManager>().abilities["X"].SecondaryCall();
            }

            // Ability 2
            if (Input.GetButtonDown("Y") && GetComponent<ItemAbilityManager>().abilities.ContainsKey("Y"))
            {
                GetComponent<ItemAbilityManager>().abilities["Y"].PrimaryCall();
            }
            if (Input.GetButtonUp("Y") && GetComponent<ItemAbilityManager>().abilities.ContainsKey("Y"))
            {
                GetComponent<ItemAbilityManager>().abilities["Y"].SecondaryCall();
            }

            // Ability 3
            if (Input.GetButtonDown("B") && GetComponent<ItemAbilityManager>().abilities.ContainsKey("B"))
            {
                GetComponent<ItemAbilityManager>().abilities["B"].PrimaryCall();
            }
            if (Input.GetButtonUp("B") && GetComponent<ItemAbilityManager>().abilities.ContainsKey("B"))
            {
                GetComponent<ItemAbilityManager>().abilities["B"].SecondaryCall();

            }
        }
    }
}