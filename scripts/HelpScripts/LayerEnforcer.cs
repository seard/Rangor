using UnityEngine;
using System.Collections;

public class LayerEnforcer : MonoBehaviour
{

    public LayerMask myLayer;
    public bool setChildren = false;

    // Use this for initialization
    void Awake()
    {
        if (setChildren)
            SetLayerRecursively(gameObject, myLayer);
        else
            gameObject.layer = myLayer;
    }

    void Start()
    {
        if (setChildren)
            SetLayerRecursively(gameObject, myLayer);
        else
            gameObject.layer = myLayer;
    }

    void OnEnable()
    {
        if (setChildren)
            SetLayerRecursively(gameObject, myLayer);
        else
            gameObject.layer = myLayer;
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (null == obj)
            return;

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
            SetLayerRecursively(child.gameObject, newLayer);
    }
}
