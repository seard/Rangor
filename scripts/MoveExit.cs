using UnityEngine;
using System.Collections;

public class MoveExit : MonoBehaviour {
    public Transform exit;
    public float timeToMove = 5;
    private float timer = 0;

	void Update () 
    {
        timer += Time.deltaTime;

        if (timer >= timeToMove)
        {
            Vector3 randpos = new Vector3(Random.Range(0, 32), Random.Range(0, 32), 0);
            exit.transform.position = randpos;
            timer = 0;
        }
	}
}
