using UnityEngine;

public class FollowTest : MonoBehaviour
{
    private Transform target;
    public float speed = 2f;
    public float gravity = -9.81f;
    private Vector3 velocity = Vector3.zero;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private CharacterController controller;
    private float verticalVelocity = 0f;
    private bool isGrounded;

    private float bobFrequency = 2f;
    private float bobAmplitude = 0.2f;

    public void Activate()
    {
        controller = GetComponent<CharacterController>();

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }

        enabled = true;
    }

    void OnEnable()
    {
        controller = GetComponent<CharacterController>();

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        //verticalVelocity.y += verticalVelocity * Time.deltaTime;

        //Vector3 direction = (target.position - transform.position).normalized;
        //Vector3 move = direction * speed * Time.deltaTime;
        //controller.Move(move + verticalVelocity * Time.deltaTime);

        //Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);

        Vector3 toTarget = (target.position - transform.position).normalized;
        Vector3 desiredVelocity = toTarget * speed;
        Vector3 steering = desiredVelocity - velocity;

        float maxSteering = 10f;

        steering = Vector3.ClampMagnitude(steering, maxSteering * Time.deltaTime);
        velocity += steering;

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 finalMove = velocity;
        finalMove.y = verticalVelocity;

        controller.Move(finalMove * Time.deltaTime);

        Vector3 horizontalVelocity = new Vector3(velocity.x, 0, velocity.z);

        if (horizontalVelocity.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(horizontalVelocity);
            float turnSpeed = 5f;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
        }

        float bobY = Mathf.Sin(Time.time * bobFrequency) * bobAmplitude;
        Vector3 bobbedPosition = transform.position + new Vector3(0, bobY, 0);
        transform.GetChild(0).localPosition = new Vector3(0, bobY, 0);
    }
}

