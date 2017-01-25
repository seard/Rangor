using UnityEngine;
using System.Collections;

// Call by GetComponent<CameraShake>().Shake(1)

public class CameraShake : MonoBehaviour {

    static bool Shaking;
    static float ShakeDecay;
    static float ShakeIntensity;
    static Vector3 ShakeSize;

    void Update()
    {
        ShakeUpdate();
    }

    public static void Shake(float _power)
    {
        ShakeIntensity = _power * 0.3f;
        ShakeDecay = 0.02f;
        Shaking = true;
    }

    void ShakeUpdate()
    {
        if (Shaking)
        {
            ShakeSize = new Vector3(Random.Range(-ShakeIntensity, ShakeIntensity), Random.Range(-ShakeIntensity, ShakeIntensity));
            transform.position += ShakeSize;
            ShakeIntensity -= ShakeDecay;
        }
        if (ShakeIntensity <= 0)
        {
            Shaking = false;
            ShakeSize = Vector3.zero;
            ShakeIntensity = 0;
        }
    }
}
