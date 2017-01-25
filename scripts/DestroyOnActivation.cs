using UnityEngine;
using System.Collections;

public class DestroyOnActivation : MonoBehaviour
{
    void OnEnable()
    {
        Destroy(gameObject);
    }
}
