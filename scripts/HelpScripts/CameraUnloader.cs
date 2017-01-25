using UnityEngine;
using System.Collections;

public class CameraUnloader : MonoBehaviour
{
	// Deactivates camera for faster resource unloading
	void OnApplicationQuit()
    {
		gameObject.SetActive(false);
	}
}
