using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NetworkView))]

public class TargetIndicatorController : MonoBehaviour
{
    static GameObject FrontCircleIndicatorObject;
    static GameObject AOECircleIndicatorObject;
    static GameObject FrontLineIndicatorObject;
    static GameObject FrontConeIndicatorObject;

    static SpriteRenderer FrontCircleRenderer;
    static SpriteRenderer AOECircleRenderer;
    static SpriteRenderer FrontLineRenderer;
    static SpriteRenderer FrontConeRenderer;

    static bool FrontCircleInUse = false;
    static bool AOECircleInUse = false;
    static bool FrontLineInUse = false;
    static bool FrontConeInUse = false;

    public Color defaulIndicatorColor;
    static Color indicatorColor;

    static PlayerController controller;

    static Transform thisPlayer;
    static Transform thatPlayer;

    static Vector3 thisColliderPos;
    static Vector3 thatColliderPos;

	// Use this for initialization
	void Start ()
    {
        FrontCircleIndicatorObject = transform.FindChild("FrontCircleIndicator").gameObject;
        AOECircleIndicatorObject = transform.FindChild("AOECircleIndicator").gameObject;
        FrontLineIndicatorObject = transform.FindChild("FrontLineIndicator").gameObject;
        FrontConeIndicatorObject = transform.FindChild("FrontConeIndicator").gameObject;

        FrontCircleRenderer = FrontCircleIndicatorObject.GetComponent<SpriteRenderer>();
        AOECircleRenderer = AOECircleIndicatorObject.GetComponent<SpriteRenderer>();
        FrontLineRenderer = FrontLineIndicatorObject.GetComponent<SpriteRenderer>();
        FrontConeRenderer = FrontConeIndicatorObject.GetComponent<SpriteRenderer>();

        indicatorColor = defaulIndicatorColor;

        thisPlayer = transform.parent.parent;
        controller = GameObject.FindGameObjectWithTag("Will").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.T))
            Debug.Log(TargetIndicatorController.FrontLineIndicator(10.0f));
        if (Input.GetKey(KeyCode.Y))
            Debug.Log(TargetIndicatorController.FrontConeIndicator());
        if (Input.GetKey(KeyCode.U))
            Debug.Log(TargetIndicatorController.FrontCircleIndicator());
        if (Input.GetKey(KeyCode.I))
            Debug.Log(TargetIndicatorController.AOECircleIndicator());
        if (!thatPlayer)
            if (GetNetPlayer.GetThatPlayer())
                thatPlayer = GetNetPlayer.GetThatPlayer();

        // Getting collider center of players
        thisColliderPos = thisPlayer.GetComponent<Collider2D>().bounds.center;
        if(thatPlayer)
            thatColliderPos = thatPlayer.GetComponent<Collider2D>().bounds.center;

        //FadeIndicators();
	}

    void FixedUpdate()
    {
        FadeIndicators();
    }

    public static bool FrontCircleIndicator(float _radius = 1.0f, float _distance = 1.0f)
    {
        FrontCircleIndicatorObject.SetActive(true);
        FrontCircleRenderer.color = indicatorColor;

        //
        FrontCircleIndicatorObject.transform.localScale = Vector3.one * _radius;

        FrontCircleIndicatorObject.transform.localPosition = Vector3.Scale(controller.FacingDirection3() * _distance, controller.transform.localScale);

        if (Vector2.Distance(thatColliderPos, FrontCircleIndicatorObject.transform.position) <= _radius)
            return true;
        //
        return false;
    }
    public static bool AOECircleIndicator(float _radius = 3.0f)
    {
        AOECircleIndicatorObject.SetActive(true);
        AOECircleRenderer.color = indicatorColor;

        thisColliderPos = thisPlayer.GetComponent<Collider2D>().bounds.center;
        thatColliderPos = thatPlayer.GetComponent<Collider2D>().bounds.center;

        //
        AOECircleIndicatorObject.transform.localScale = Vector3.one * _radius;

        if (Vector2.Distance(thisColliderPos, thatColliderPos) <= _radius)
            return true;
        //
        return false;
    }
    public static bool FrontLineIndicator(float _length = 5.0f, bool _hitColliders = true)
    {
        FrontLineIndicatorObject.SetActive(true);
        FrontLineRenderer.color = indicatorColor;

        //
        // Get facingdirection for multiple uses
        Vector3 facingDirection = controller.FacingDirection3();

        // Calculate rotation
        float rotationZ = Quaternion.LookRotation(new Vector3(facingDirection.x, 0, facingDirection.y)).eulerAngles.y;
        FrontLineIndicatorObject.transform.localEulerAngles = new Vector3(0, 0, rotationZ * -thisPlayer.localScale.x);

        // What the line indicators hits
        RaycastHit2D hit = controller.RayCastHit(facingDirection, _length);

        if (hit)
        {
            float finalLength = _length;

            if (_hitColliders)
                finalLength = Vector2.Distance(hit.point, thisColliderPos);

            // Set length of line indicator
            FrontLineIndicatorObject.transform.localScale = new Vector3(1, finalLength, 1);

            if (hit.transform.tag == thatPlayer.tag)
                return true;
        }

        return false;
    }
    public static bool FrontConeIndicator(float _angle = 90.0f, float _radius = 2.0f)
    {
        FrontConeIndicatorObject.SetActive(true);
        FrontConeRenderer.color = indicatorColor;
        FrontConeIndicatorObject.transform.localScale = Vector3.one * _radius;

        // Clamp radius
        if (_radius > 100)
            _radius = 100;
        if (_radius < 1)
            _radius = 1;

        // Clamp angle
        if (_angle > 180)
            _angle = 180;
        if (_angle < 0)
            _angle = 0;

        // Calculations need to be done for half the cone (sin(A) where A > 90 is gonna start decreasing)
        _angle /= 2.0f;

        // Get facingdirection for multiple uses
        Vector3 facingDirection = controller.FacingDirection3();

        FrontConeIndicatorObject.transform.localScale = Vector3.one * _radius;

        // Calculate rotation
        float rotationZ = Quaternion.LookRotation(new Vector3(facingDirection.x, 0, facingDirection.y)).eulerAngles.y;
        FrontConeIndicatorObject.transform.localEulerAngles = new Vector3(0, 0, rotationZ * -thisPlayer.localScale.x);

        SpriteRenderer coneRenderer = FrontConeIndicatorObject.GetComponent<SpriteRenderer>();

        coneRenderer.material.SetFloat("_Angle", _angle);
        coneRenderer.material.SetColor("_Color", coneRenderer.color);

        //
        // If thatPlayer is within radius
        if (Vector3.Distance(thisColliderPos, thatColliderPos) < _radius)
        {
            // I'm not gonna bother explaining, but this is correct
            Vector3 vectorToThatPlayer = (thatColliderPos - thisColliderPos).normalized;

            if (Vector2.Angle(facingDirection, vectorToThatPlayer) <= _angle)
            {
                return true;
            }
        }
        //
        return false;
    }

    public static void FadeIndicators()
    {
        Color fade = new Color(indicatorColor.r, indicatorColor.g, indicatorColor.b, 0);

        if (FrontCircleRenderer.color.a > 0.01f)
            FrontCircleRenderer.color = Color.Lerp(FrontCircleRenderer.color, fade, Time.deltaTime);
        if (AOECircleRenderer.color.a > 0.01f)
            AOECircleRenderer.color = Color.Lerp(AOECircleRenderer.color, fade, Time.deltaTime);
        if (FrontLineRenderer.color.a > 0.01f)
            FrontLineRenderer.color = Color.Lerp(FrontLineRenderer.color, fade, Time.deltaTime);
        if (FrontConeRenderer.color.a > 0.01f)
        {
            FrontConeRenderer.color = Color.Lerp(FrontConeRenderer.color, fade, Time.deltaTime * 0.5f);
            FrontConeIndicatorObject.GetComponent<SpriteRenderer>().material.SetColor("_Color", FrontConeRenderer.color);
        }
    }

    public static void DeactivateFrontCircleIndicator()
    {
        FrontCircleIndicatorObject.SetActive(false);
    }
    public static void DeactivateAOECircleIndicator()
    {
        AOECircleIndicatorObject.SetActive(false);
    }
    public static void DeactivateFrontLineIndicator()
    {
        FrontLineIndicatorObject.SetActive(false);
    }
    public static void DeactivateFrontConeIndicator()
    {
        FrontConeIndicatorObject.SetActive(false);
    }
}