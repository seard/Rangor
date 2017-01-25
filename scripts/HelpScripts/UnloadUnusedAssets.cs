using UnityEngine;
using System.Collections;

public class UnloadUnusedAssets : MonoBehaviour {

    public float UpdateRate = 5.0f;
    float time = 0;

	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if(time >= UpdateRate)
        {
            Resources.UnloadUnusedAssets();
            time = 0;
        }
	}
}
