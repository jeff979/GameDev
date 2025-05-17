using UnityEngine;
using UnityEngine.UI;

public class Special : MonoBehaviour
{
    public static Special Instance;

    public Slider chargeSlider;
    public float maxCharge = 1f;
    public float currentCharge = 0f;

    private void Awake()
    {
        Instance = this;
    }

    public void AddCharge(float amount)
    {
        currentCharge += amount;
        currentCharge = Mathf.Clamp(currentCharge, 0f, maxCharge);
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (chargeSlider != null)
            chargeSlider.value = currentCharge / maxCharge;
    }

    public bool IsFull()
    {
        return currentCharge >= maxCharge;
    }

    public void UseCharge()
    {
        //Debug.Log("hit");
        if (IsFull())
        {
            currentCharge = 0f;
            UpdateUI();
        }
    }
}