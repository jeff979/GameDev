using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;

    public Slider healthBar;
    void Start()
    {
        currentHealth = maxHealth;
        updateHealthUI();
        
    }

    void updateHealthUI()
    {
        float fillAmount = (float)currentHealth / maxHealth;
        healthBar.value = fillAmount;

    }

    public void DamagePlayer(int damage)
    {

        //Debug.Log("Taking damage: " + damage);
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        updateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    { 
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        updateHealthUI();
    }

    void Die()
    {
        Debug.Log("Player Died");
        FindObjectOfType<GameManager>().TriggerGameOver();

        MonoBehaviour movementScript = GetComponent<FPSMovement>();
        if (movementScript != null) movementScript.enabled = false;

        Transform cameraTransform = transform.Find("MainCamera");
        if (cameraTransform != null)
        {
            MouseLook mouseLook = cameraTransform.GetComponent<MouseLook>();
            if (mouseLook != null) mouseLook.enabled = false;
        }
    }
}
