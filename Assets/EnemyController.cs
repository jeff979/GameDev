using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float fallSpeed = 18f;
    public GameObject trailEffectPrefab;
    private bool isFalling = true;
    private GameObject trailEffect;
    public LayerMask groundMask;

    private Transform groundCheck;
    public float groundDistance = 0.4f;

    public int damage = 20;

    void Start()
    {
        groundCheck = transform.Find("groundCheckEnemy");
    }

    void Update()
    {
        if (isFalling)
        {
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;

            bool isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded)
            {
                isFalling = false;
                Destroy(trailEffect);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            //Debug.Log("Player hit!");
            //endScreen.gameObject.SetActive(true);

            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            { 
                playerHealth.DamagePlayer(damage);
                EnemyDeathEffect deathEffect = GetComponent<EnemyDeathEffect>();

                if (deathEffect != null)
                {
                    deathEffect.TriggerDeathEffect();
                    GetComponent<Collider>().enabled = false;
                    FollowTest follow = GetComponent<FollowTest>();
                    if (follow != null) follow.enabled = false;
                    CharacterController controller = GetComponent<CharacterController>();
                    if (controller != null) controller.enabled = false;
                }
            }
        }
    }
}
