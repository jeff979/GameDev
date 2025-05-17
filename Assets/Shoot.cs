using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;

    public float fireRate = 0.5f;
    private float nextFire = 0f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            Fire();
            nextFire = Time.time + fireRate;
        }
    }

    void Fire()
    {
        Instantiate(projectilePrefab, firePoint.position, Camera.main.transform.rotation);
    }
}

