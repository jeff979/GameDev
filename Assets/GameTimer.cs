using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{

    public Text timer;
    private float timePassed = 0f;
    private bool isRunning = true;

    void Update()
    {
        if(isRunning)
        {
            timePassed += Time.deltaTime;
            int minutes = Mathf.FloorToInt(timePassed / 60f);
            int seconds = Mathf.FloorToInt(timePassed % 60f);
            timer.text = $"{minutes:00}:{seconds:00}";
        }
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public float GetTimePassed()
    {
        return timePassed;
    }
}
