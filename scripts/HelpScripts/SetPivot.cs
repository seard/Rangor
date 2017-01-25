using UnityEngine;
using System.Collections;

[ExecuteInEditMode]

public class SetPivot : MonoBehaviour {

    public Vector3 pivot;

    Vector3 oldPosition;

	#if UNITY_EDITOR
	void Start ()
    {
        transform.position += pivot;
        this.enabled = false;
	}
    #endif
}
