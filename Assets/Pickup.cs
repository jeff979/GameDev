using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float chargeValue = 0.2f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.Rotate(Vector3.up * 45f * Time.deltaTime);
        transform.position = startPos + Vector3.up * Mathf.Sin(Time.time * 2f) * 0.4f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Special.Instance.AddCharge(chargeValue);
            Destroy(gameObject);
        }
    }
}