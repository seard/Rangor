using UnityEngine;
using System.Collections;

public class MinimapCharacterController : MonoBehaviour {
    public Transform player;
    Vector3 posXY;

	void Update () 
    {
        posXY = new Vector3(transform.position.x - player.position.x, transform.position.y - player.position.y, 0).normalized;
        posXY += player.position;
        transform.position = new Vector3(posXY.x, posXY.y, transform.position.z);
	}
}
