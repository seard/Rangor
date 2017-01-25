using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public Transform player;
	public float maxPlayerDistance = 2.0f;
	public float zoomOut = 6;
    public float snapSize = 32.0f;
    public float camShiftAmount = 0.0f;

    Vector3 camShift;

    float oldZoomOut;

	// Use this for initialization
	void Start () {
		this.GetComponent<Camera>().orthographicSize = zoomOut;
	}

	void Update()
    {
        if (GetComponent<FOW_Effect>())
            snapSize = ((GetComponent<FOW_Effect>().screenResolution) / 16) / (zoomOut / 8);

        if(oldZoomOut != zoomOut)
        {
            oldZoomOut = zoomOut;
            GetComponent<Camera>().orthographicSize = zoomOut;
            if (GetComponent<FOW_Effect>().material)
                GetComponent<FOW_Effect>().material.SetFloat("_orthoSize", zoomOut);

            // Set children camera size to this camera size
            if (transform.childCount != 0)
            {
                foreach (Transform child in transform)
                {
                    if(child.GetComponent<Camera>())
                        child.GetComponent<Camera>().orthographicSize = zoomOut;
                }
            }
        }

        if (Input.GetJoystickNames().Length > 0) // If controller is connected, calculated extra shift depending on joystick movement
            camShift = Vector3.Lerp(camShift, new Vector3(Input.GetAxis("RightStickX") * camShiftAmount, Input.GetAxis("RightStickY") * camShiftAmount, 0), Time.deltaTime * 4);
        else // Else set camshift to 0 if ex. player pulls out controller
            camShift = Vector3.zero;

        // Apply camshift
        transform.position += camShift;

        // If player is outside of maximum range
		if(Vector2.Distance(transform.position - camShift, player.position) >= maxPlayerDistance)
		{
            // Calculate cameras position on circle
			Vector3 pos = new Vector3(transform.position.x - player.position.x, transform.position.y - player.position.y, 0).normalized;
			pos *= maxPlayerDistance;
            pos += player.position + camShift;
            
            // Snap camera to pixel decimal positions
            if (snapSize != 0)
            {
                Vector2 posFloor = new Vector2(Mathf.Round(pos.x * snapSize) / snapSize, Mathf.Round(pos.y * snapSize) / snapSize);
                //Vector2 posFloor = new Vector2(Mathf.Round(pos.x * Screen.width) / Screen.width, Mathf.Round(pos.y * Screen.height) / Screen.height);
                transform.position = new Vector3(posFloor.x, posFloor.y, transform.position.z); // Adds shake addition
            }
            else
                transform.position = new Vector3(pos.x, pos.y, transform.position.z); // Adds shake addition
		}
	}
}
