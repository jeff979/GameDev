using UnityEngine;

public class Sword : MonoBehaviour
{
    public Transform playerCamera; 
    public Transform HandPosition; 
    public Transform restingSword;
    public Transform swingingSword;

    public float swingSpeed = 3f;
    public float holdSwingSpeed = 0.5f;
    private float currentSpeed;

    public float swingArc = 160f; 
    public float swingDuration = 0.3f;
    public float trackingTime = 0.1f; 

    private Vector3 pastLookDirection;
    private Vector3 currentLookDirection; 
    private bool isSwinging = false;
    private float swingProgress = 0f;
    private Vector3 swingAxis;

    private bool trailActivated = false;
    public TrailRenderer fastTrail;
    public TrailRenderer slowTrail;

    public GameObject crosshairDot;
    public RectTransform crosshairLine;
    private Vector3 crosshairLookDirection;
    private Vector3 crosshairSwingAxis; 

    private float swingHoldTime = 0f;
    private bool holding = false;
    public float holdThreshold = 0.2f;
    private bool holdStarted = false;

    void Start()
    {
        pastLookDirection = playerCamera.forward;

        crosshairDot.SetActive(true);
        crosshairLine.gameObject.SetActive(true);
        fastTrail.emitting = false;
        slowTrail.emitting = false;
    }

    void Update()
    {

        //pastLookDirection = Vector3.Lerp(pastLookDirection, playerCamera.forward, Time.deltaTime / trackingTime);

        //if (Input.GetButtonDown("Fire1") && !isSwinging)
        //{
        //    BeginSwing();
        //}

        //if (Input.GetButtonDown("Fire1") && !isSwinging)
        //{
        //    swingHoldTime = 0f;
        //    holding = true;
        //    BeginSwing();
        //}

        //if (Input.GetButton("Fire1") && holding)
        //{
        //    swingHoldTime += Time.deltaTime;
        //}

        //if (Input.GetButtonUp("Fire1"))
        //{
        //    holding = false;           
        //}

        if (!isSwinging)
        {
            // Start tracking hold on click
            if (Input.GetButtonDown("Fire1"))
            {
                holding = true;
                swingHoldTime = 0f;
                holdStarted = false;
            }

            // Track how long mouse is held
            if (Input.GetButton("Fire1") && holding)
            {
                swingHoldTime += Time.deltaTime;

                pastLookDirection = Vector3.Lerp(pastLookDirection, playerCamera.forward, Time.deltaTime / trackingTime);
                if (!holdStarted && swingHoldTime >= holdThreshold)
                {
                    // Begin slow swing
                    BeginSlowSwing();
                    isSwinging = true;
                    holdStarted = true;
                }
            }

            // Released before threshold = fast swing
            if (Input.GetButtonUp("Fire1") && holding)
            {
                pastLookDirection = Vector3.Lerp(pastLookDirection, playerCamera.forward, Time.deltaTime / trackingTime);
                if (!holdStarted)
                {
                    BeginFastSwing();
                    isSwinging = true;
                }

                // If it *was* a slow swing, this will cancel it
                holding = false;
            }
        }
        else
        {
            // If it's a slow swing, and player releases, cancel it
            if (holdStarted && Input.GetButtonUp("Fire1"))
            {
                EndSwing(); // Cancel mid-slow-swing
            }

            UpdateSwing(); // Advance swing logic
        }

        UpdateCrosshair();
    }

    void BeginFastSwing()
    {
        swingDuration = 0.4f;
        swingArc = 120f;
        holdStarted = false;
        BeginSwing();
    }

    void BeginSlowSwing()
    {
        swingDuration = 2f;
        swingArc = 160f;
        holdStarted = true;
        BeginSwing();
    }

    void BeginSwing()
    {
        isSwinging = true;
        swingProgress = 0f;

        currentLookDirection = playerCamera.forward;
        
        restingSword.gameObject.SetActive(false);
        swingingSword.gameObject.SetActive(true);
        trailActivated = false;

        swingingSword.transform.position = HandPosition.position;
        swingingSword.transform.rotation = Quaternion.LookRotation(pastLookDirection, swingAxis);

        swingAxis = Vector3.Cross(pastLookDirection, currentLookDirection).normalized;
    }

    void UpdateSwing()
    {
        swingProgress += Time.deltaTime / swingDuration;

        //swingProgress += Time.deltaTime * currentSpeed;

        if (!trailActivated && swingProgress > 0.05f)
        {
            trailActivated = true;

            if (holdStarted)
            {
                slowTrail.Clear();
                slowTrail.emitting = true;
            }
            else
            {
                fastTrail.Clear();
                fastTrail.emitting = true;
            }
        }

        if (swingProgress >= 1f)
        {
            EndSwing();
            return;
        }

        float swingAngle = -Mathf.Lerp(-swingArc, swingArc, swingProgress);
        swingingSword.transform.rotation = Quaternion.AngleAxis(swingAngle, swingAxis) * Quaternion.LookRotation(pastLookDirection, swingAxis);
        //swingingSword.transform.rotation = Quaternion.AngleAxis(swingAngle, swingAxis) * Quaternion.LookRotation(pastLookDirection);

    }

    void EndSwing()
    {
        isSwinging = false;
        holding = false;
        holdStarted = false;
        fastTrail.emitting = false;
        slowTrail.emitting = false;
        trailActivated = false;

        restingSword.gameObject.SetActive(true);
        swingingSword.gameObject.SetActive(false);
    }

    //void UpdateCrosshair()
    //{
    //    crosshairLookDirection = playerCamera.forward;
    //    crosshairSwingAxis = Vector3.Cross(pastLookDirection, currentLookDirection).normalized;
    //    Vector3 crosshairSwingDirection = crosshairSwingAxis.normalized;

    //    float angle = Mathf.Atan2(crosshairSwingDirection.x, crosshairSwingDirection.y) * Mathf.Rad2Deg;

    //    crosshairLine.rotation = Quaternion.Euler(0, 0, angle);
    //}

    void UpdateCrosshair()
    {
        Vector3 cameraMovementDirection = playerCamera.forward - pastLookDirection;

        cameraMovementDirection.y = 0;
        cameraMovementDirection.Normalize();

        float angle = Mathf.Atan2(cameraMovementDirection.x, cameraMovementDirection.z) * Mathf.Rad2Deg;

        crosshairLine.rotation = Quaternion.Euler(0, 0, angle);
    }
}
