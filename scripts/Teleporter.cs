using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform exit;
    static Transform last;

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Sten");
        TeleportToExit(other);
    }

    void TeleportToExit(Collision2D other)
    {
        other.transform.position = exit.transform.position;
    }

}