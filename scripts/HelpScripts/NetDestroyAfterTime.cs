using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NetworkView))]

public class NetDestroyAfterTime : MonoBehaviour {

    public float waitTime = 10.0f;
    float timer = 0;

    bool usingNetwork = false;

	void Update ()
    {
        timer += Time.deltaTime;

        if (timer >= waitTime)
        {
            Network.Destroy(gameObject);
        }
	}
}
