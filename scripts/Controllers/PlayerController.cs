using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    Vector2 dirVector;
    Vector2 dirVectorSet;
    Vector2 facingDirVector = -Vector2.up;
    public Vector2 additiveDirVector;

    public bool invertControls = false;
    public bool lockControls = false;
    public float speed = 200.0f;
    public float acceleration = 0.0f;
    public float wallFriction = 0.5f;

    public LayerMask collidable;
    float rayLength;

    Vector2 lastPosition;
    Vector2 deltaPosition;
    public float realSpeed;

    public bool isNetwork = false;

    // Use this for initialization
    void Start()
    {
        // Initialize rigidbody2D
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Rigidbody2D>().fixedAngle = true;
    }

    public void Move(Vector2 _dirVector)
    {
        dirVectorSet += _dirVector * (invertControls ? -1 : 1);
    }
    public void ForceMove(Vector2 _dirVector, float _duration = 0, bool _killOnZeroVelocity = false, bool _lockControls = false)
    {
        StartCoroutine(CreateMoveInstance(_dirVector, _duration, _killOnZeroVelocity, _lockControls));
    }
    IEnumerator CreateMoveInstance(Vector2 _dirVector, float _duration, bool _killOnZeroVelocity, bool _lockControls)
    {
        // Lock controls
        if (_lockControls)
            lockControls = true;

        float tick = _duration;

        // If the movement is not to be continuos
        if (_duration == 0)
        {
            additiveDirVector += _dirVector;
        }
        // Else enter while-loop to continously add _dirVector
        else
        {
            while (tick > 0)
            {
                // Kills movement if standing still, ex. hitting a wall
                if (_killOnZeroVelocity && !IsMoving())
                {
                    tick = 0;
                }

                additiveDirVector += _dirVector;
                tick -= Time.fixedDeltaTime;

                yield return new WaitForFixedUpdate();
            }
        }

        // Unlock controls
        if (_lockControls && _duration != 0)
            lockControls = false;
    }
    public Vector2 GetDirection()
    {
        return dirVector;
    }
    public Vector3 GetDirection3()
    {
        return dirVector;
    }
    public Vector3 GetAdditiveDirVector()
    {
        return additiveDirVector;
    }
    public void SetDirection(Vector2 _dirVector)
    {
        dirVector = _dirVector;
    }
    public void SetAdditiveDirVector(Vector2 _dirVector)
    {
        additiveDirVector = _dirVector;
    }
    // Returns direction character is facing
    public Vector2 FacingDirection()
    {
        return facingDirVector;
    }
    public Vector3 FacingDirection3()
    {
        return facingDirVector;
    }
    // Returns simplified 8-direction string
    public string FacingDirectionSimple()
    {
        if (facingDirVector.x > 0.38f && facingDirVector.y > 0.38f)
            return "NE";
        if (facingDirVector.x < -0.38f && facingDirVector.y > 0.38f)
            return "NW";

        if (facingDirVector.x > 0.38f && facingDirVector.y < -0.38f)
            return "SE";
        if (facingDirVector.x < -0.38f && facingDirVector.y < -0.38f)
            return "SW";

        if (facingDirVector.y > 0.707f)
            return "N";
        if (facingDirVector.x > 0.707f)
            return "E";
        if (facingDirVector.y < -0.707f)
            return "S";
        if (facingDirVector.x < -0.707f)
            return "W";

        return null;
    }
    public float GetVelocity()
    {
        return GetComponent<Rigidbody2D>().velocity.magnitude;
    }
    public float GetSpeed()
    {
        return realSpeed;
    }
    public bool IsMoving()
    {
        if (GetVelocity() <= 0.01f)
            return false;
        return true;
    }

    // Calculates correct movement direction with outside forces
    void Controls()
    {
        if (wallFriction < 0)
            wallFriction = 0;
        if (acceleration < 0)
            acceleration = 0;

        rayLength = GetComponent<CircleCollider2D>().radius * 1.05f;

        if(!isNetwork)
            dirVector = dirVectorSet.normalized;

        // Set what direction player is facing
        if (dirVector.magnitude != 0)
            facingDirVector = dirVector;

        // Begin calculating friction and general movement
        if (dirVector.y > 0) // If input is Up
            if (RayCast(Vector2.up, rayLength)) // If obstacle above
                dirVector = new Vector2(dirVector.x, 1.0f - ((1.0f - wallFriction))); // If obstacle, set friction on walls
        if (dirVector.y < 0) // If input is Down
            if (RayCast(-Vector2.up, rayLength)) // If obstacle below
                dirVector = new Vector2(dirVector.x, -1.0f + (1.0f - wallFriction));
        if (dirVector.x > 0) // If input is Right
            if (RayCast(Vector2.right, rayLength)) // If obstacle to the right
                dirVector = new Vector2(1.0f - (1.0f - wallFriction), dirVector.y);
        if (dirVector.x < 0) // If input is Left
            if (RayCast(-Vector2.right, rayLength)) // If obstacle to the left
                dirVector = new Vector2(-1.0f + (1.0f - wallFriction), dirVector.y);

        if (dirVector.x > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (dirVector.x < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    void FixedUpdate()
    {
        if (!lockControls)
            Controls();

        Debug.DrawRay(GetComponent<Collider2D>().bounds.center, facingDirVector, Color.yellow);
        Debug.DrawRay(GetComponent<Collider2D>().bounds.center, dirVector, Color.red);
        Debug.DrawRay(GetComponent<Collider2D>().bounds.center, additiveDirVector, Color.blue);

        Vector2 dirVectorCopy = dirVector;

        // If lockControls, set dirVector to 0
        dirVectorCopy *= (lockControls ? 0 : 1);

        // Calculate ending direction to move in
        Vector2 dirVectorCalc = dirVectorCopy.normalized + additiveDirVector;

        // If acceleration is outside interval 0-10, use instant acceleration
        if (acceleration == 0 || acceleration >= 10)
            GetComponent<Rigidbody2D>().velocity = dirVectorCalc * Time.fixedDeltaTime * speed;
        // Else use acceleration
        else
            GetComponent<Rigidbody2D>().velocity = Vector2.Lerp(GetComponent<Rigidbody2D>().velocity, dirVectorCalc * speed * Time.fixedDeltaTime, Time.fixedDeltaTime * acceleration);

        if (!isNetwork)
        {
            dirVectorSet = Vector2.zero;
            additiveDirVector = Vector2.zero;
        }

        realSpeed = ((Vector2)transform.position - lastPosition).magnitude;
        lastPosition = transform.position;
	}   

    // Simplified raycast that ignores <collidable> layerMask
    public bool RayCast(Vector2 _direction, float _distance = 1.0f)
    {
        return Physics2D.Raycast(GetComponent<Collider2D>().bounds.center, _direction, _distance, collidable);
    }

    // Simplified raycast that ignores <collidable> layerMask, returns collided gameObject
    public GameObject RayCastObject(Vector2 _direction, float _distance = 1.0f)
    {
        RaycastHit2D hit = Physics2D.Raycast(GetComponent<Collider2D>().bounds.center, _direction, _distance, collidable);
        if (hit)
            return hit.transform.gameObject;
        return null;
    }

    // Casts ray from just outside own collider radius and returns complete hit
    public RaycastHit2D RayCastHit(Vector2 _direction, float _distance = 1.0f)
    {
        return Physics2D.Raycast((Vector2)GetComponent<Collider2D>().bounds.center + (_direction.normalized * 0.5f), _direction, _distance);
    }

    // Returns true if colliding with object up, down, left or right
    public bool isColliding()
    {
        if (RayCast(Vector2.up, rayLength))
            return true;
        else if (RayCast(-Vector2.up, rayLength))
            return true;
        else if (RayCast(Vector2.right, rayLength))
            return true;
        else if (RayCast(-Vector2.right, rayLength))
            return true;
        else
            return false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            lockControls = false;

        if (Input.GetKey(KeyCode.G))
            if (TargetIndicatorController.FrontCircleIndicator())
                Debug.Log("funkar");
    }
}