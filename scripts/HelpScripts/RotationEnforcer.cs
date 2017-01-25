using UnityEngine;
using System.Collections;

public class RotationEnforcer : MonoBehaviour {

    // Use this for initialization
    void Update()
    {
        transform.eulerAngles = Vector3.zero;
    }
}
