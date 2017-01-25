using UnityEngine;
using System.Collections;

public class FollowGameObject : MonoBehaviour
{
    void Update()
    {
        UpdatePos();
    }

    public void UpdatePos()
    {
        transform.position = gameobject.transform.position;
    }
}
