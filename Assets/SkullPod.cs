using UnityEngine;

public class SkullPod : MonoBehaviour
{
    public float fallSpeed = 15f;
    private bool isFalling = false;
    public LayerMask groundMask;

    private Transform groundCheck;
    private Transform skullModel;
    public float groundDistance = 0.6f;

    public float embedOffset = 0.3f;
    public float fadeDelay = 2f;
    public float fadeSpeed = 1f;

    private bool fading = false;
    private float fadeProgress = 0f;
    private Material podMaterial;

    public GameObject enemyPrefab;
    public Transform spawnPoint;
    private WaveManager waveManager;

    private Transform player;

    public void Initialise(WaveManager manager, Transform playerTransform)
    {
        waveManager = manager;
        player = playerTransform;

    }

    void Start()
    {
        skullModel = transform.Find("skull");
        groundCheck = transform.Find("groundCheck");
        Renderer rend = GetComponentInChildren<Renderer>();
        if (rend != null)
        {
            podMaterial = new Material(rend.material); 
            rend.material = podMaterial;
        }
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 offset = Random.insideUnitSphere * 4f;
            offset.y = 0;
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position + offset, Quaternion.identity);

            FollowTest follow = enemy.GetComponent<FollowTest>();
            if (follow != null) follow.Activate();

            if (waveManager != null)
                waveManager.RegisterEnemy(enemy);
        }
    }

    public void StartFalling()
    {
        isFalling = true;
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

                transform.position -= new Vector3(0, embedOffset, 0);

                SpawnEnemies();
                Invoke(nameof(StartFading), fadeDelay);

            }

            if (player != null && skullModel != null)
            {
                Vector3 lookDirection = player.position - skullModel.position;
                if (lookDirection != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                    skullModel.rotation = Quaternion.Slerp(skullModel.rotation, targetRotation, Time.deltaTime * 5f);
                }
            }
        }

        if (fading && podMaterial != null)
        {
            fadeProgress += Time.deltaTime * fadeSpeed;
            Color c = podMaterial.color;
            c.a = Mathf.Lerp(1f, 0f, fadeProgress);
            podMaterial.color = c;

            if (fadeProgress >= 1f)
                Destroy(gameObject);
        }

    }

    void StartFading()
    {
        fading = true;
    }
}