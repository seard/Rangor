using UnityEngine;
using System.Collections;

public class ScaleEnforcer : MonoBehaviour {

    // Use this for initialization
    void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x / transform.lossyScale.x, transform.localScale.y / transform.lossyScale.y, transform.localScale.z / transform.lossyScale.z);
    }
}
