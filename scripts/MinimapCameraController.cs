using UnityEngine;
using System.Collections;

public class MinimapCameraController : MonoBehaviour {

	public Transform player;
	public float maxShift = 2.0f;
	public float zoomOut = 20;
	Vector3 posXY;
	
	// Use this for initialization
	void Start ()
    {
		this.GetComponent<Camera>().orthographicSize = zoomOut;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Vector2.Distance(transform.position, player.position) >= maxShift)
		{
			posXY = new Vector3(transform.position.x - player.position.x, transform.position.y - player.position.y, 0).normalized;
			posXY *= maxShift;
			posXY += player.position;
			transform.position = new Vector3(posXY.x, posXY.y, transform.position.z);
		}
	}

}

