using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NetworkView))]

public class ExplosionController : MonoBehaviour {

    public GameObject explosionObject;

    static GameObject staticExplosionObject;

    public static void CreateExplosion(Vector2 _position)
    {
        Network.Instantiate(staticExplosionObject, _position, Quaternion.identity, 0);
    }
}