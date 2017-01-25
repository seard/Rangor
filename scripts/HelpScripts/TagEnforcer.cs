using UnityEngine;
using System.Collections;

public class TagEnforcer : MonoBehaviour
{

    public string myTag = "myTag";
    public bool setChildren = false;

    public int count;

    // Use this for initialization
    void Awake()
    {
        if (setChildren)
            SetTagRecursively(gameObject, myTag);
        else
            tag = myTag;
	}

    void Start()
    {
        if (setChildren)
            SetTagRecursively(gameObject, myTag);
        else
            tag = myTag;
    }

    void OnEnable()
    {
        if (setChildren)
            SetTagRecursively(gameObject, myTag);
        else
            tag = myTag;
    }

    void SetTagRecursively(GameObject _obj, string _tag)
    {
        if (null == _obj)
            return;

        gameObject.tag = _tag;

        foreach (Transform child in _obj.transform)
            SetTagRecursively(child.gameObject, _tag);
    }
}
