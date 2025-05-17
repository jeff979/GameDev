using UnityEngine;

public class SpecialAbility : MonoBehaviour
{
    public Transform spinningSword;
    public Transform restingSword;
    public Transform spinOrigin;
    public float spinDuration = 1f;
    public float spinSpeed = 720f;

    private bool isSpinning = false;
    private float spinTimer = 0f;

    void Update()
    {
        if (!isSpinning && Special.Instance.IsFull() && Input.GetKeyDown(KeyCode.Q))
        {
            ActivateSpin();
        }

        if (isSpinning)
        {
            //Debug.Log("spin hit");
            spinTimer += Time.deltaTime;

            spinningSword.transform.RotateAround(spinOrigin.position, Vector3.up, spinSpeed * Time.deltaTime);
            if (spinTimer >= spinDuration)
            {
                EndSpin();
            }
        }
    }

    void ActivateSpin()
    {
        //Debug.Log("special triggered");
        isSpinning = true;
        spinTimer = 0f;
        Special.Instance.UseCharge();
        restingSword.gameObject.SetActive(false);
        spinningSword.gameObject.SetActive(true);
    }

    void EndSpin()
    {
        isSpinning = false;
        spinningSword.gameObject.SetActive(false);
        restingSword.gameObject.SetActive(true);
    }
}