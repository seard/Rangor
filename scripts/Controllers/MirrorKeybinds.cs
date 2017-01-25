using UnityEngine;
using System.Collections;

public class MirrorKeybinds : MonoBehaviour
{
    // Keyboard settings

    // Joystick settings
    public float joyStickSnapSteps = 16.0f;

    // Use this for initialization
    void Start()
    {
        joyStickSnapSteps /= 8.0f;
    }

    void FixedUpdate()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            XboxSticks();
        }
        else
        {
            KeyboardControls();
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
        GetComponent<PlayerController>().Move(new Vector2(-Input.GetAxis("Horizontal"), -Input.GetAxis("Vertical")));
    }

    void XboxSticks()
    {
        // Local Movement
        Vector2 dirVector = new Vector2(-Input.GetAxis("LeftStickX"), -Input.GetAxis("LeftStickY")).normalized;
        GetComponent<PlayerController>().Move(new Vector2(Mathf.Round(dirVector.x * joyStickSnapSteps) / joyStickSnapSteps, Mathf.Round(dirVector.y * joyStickSnapSteps) / joyStickSnapSteps).normalized);
    }
}