using UnityEngine;

public class EnemyDeathEffect : MonoBehaviour
{

    public float fadeLength = 1.5f;
    public float sinkSpeed = 0.5f;
    public bool sinking = false;

    private Material material;
    private Color originalColour;
    private bool isdying = false;
    public bool hasScored = false;
    private float fadeTimer;

    public GameObject pickupPrefab;
    public float pickupDropChance = 1f; 

    void Start()
    {
        Transform skullTransform = transform.Find("skull/Skull");

        if (skullTransform != null)
        {
            Renderer rend = skullTransform.GetComponent<Renderer>();
            if (rend != null)
            {
                material = rend.material;
                originalColour = material.color;
            }
        }
    }

    //void Start()
    //{
    //    Transform skullMesh = transform.Find("skull/Skull");

    //    if (skullMesh == null)
    //    {
    //        Debug.LogError("Could not find Skull mesh at path 'Skull/Skull'");
    //        return;
    //    }

    //    Renderer rend = skullMesh.GetComponent<Renderer>();
    //    if (rend == null)
    //    {
    //        Debug.LogError("Renderer missing on Skull mesh.");
    //        return;
    //    }

    //    // Clone the material for individual instance changes
    //    material = new Material(rend.material);
    //    rend.material = material;
    //    originalColour = material.color;

    //    Debug.Log("Skull material initialized for death effect.");
    //}

    public void TriggerDeathEffect()
    {
        if (isdying) return;

        isdying = true;
        fadeTimer = fadeLength;
        material.color = Color.red;
        Invoke(nameof(StartFade), 0.1f);
    }

    void StartFade()
    {
        material.color = originalColour;
        if (sinking) return;
    }

    void Update()
    {
        if(!isdying) return;
        
        if (sinking)
        {
            transform.position -= Vector3.up * sinkSpeed * Time.deltaTime;
        }
        else
        {
            fadeTimer -= Time.deltaTime;
            float alpha = Mathf.Clamp01(fadeTimer / fadeLength);
            Color c = material.color;
            c.a = alpha;
            material.color = c;
        }

        if (fadeTimer <= 0)
        {
            if (pickupPrefab != null && Random.value <= pickupDropChance)
            {
                Vector3 dropPosition = transform.position - Vector3.up * 0.5f; ;
                Instantiate(pickupPrefab, dropPosition, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
