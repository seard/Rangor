using UnityEngine;
using System.Collections;

public class PlayerLayerController : MonoBehaviour
{
	// Update is called once per frame
	void Update () {
        if (GameObject.FindGameObjectWithTag("Deceit"))
        {
            if (transform.position.y < GameObject.FindGameObjectWithTag("Deceit").transform.position.y)
                transform.FindChild("PlayerGraphics").GetComponent<SpriteRenderer>().sortingOrder = 1;
            if (transform.position.y > GameObject.FindGameObjectWithTag("Deceit").transform.position.y)
                transform.FindChild("PlayerGraphics").GetComponent<SpriteRenderer>().sortingOrder = -1;
        }
	}
}
