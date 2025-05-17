using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject endScreen;
    public Image fadePanel;
    public Text scoreText;
    public Text timeText;

    public float fadeDuration = 1f;
    private float fadeTimer = 0f;
    private bool fading = false;

    //public Button quitButton;
    //public Button retryButton;


    //void Start()
    //{
    //    quitButton.onClick.AddListener(Quit);
    //    retryButton.onClick.AddListener(Retry);

    //}

    void Update()
    {
        if (fading)
        {
            fadeTimer += Time.deltaTime;
            float alpha = Mathf.Clamp01(fadeTimer / fadeDuration) * 0.5f;
            fadePanel.color = new Color(159f/255f, 0f, 0f, alpha);

            if (fadeTimer >= fadeDuration)
            {
                Time.timeScale = 0f;
                fading = false;
            }
        }
        
        if (endScreen.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Retry();
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                Quit();
            }
        }
    }

    public void TriggerGameOver()
    {
        endScreen.SetActive(true);
        fading = true;

        // Update score + time here
        scoreText.text = "Score: " + Score.Instance.score;
        timeText.text = "Time: " + FormatTime(Time.timeSinceLevelLoad);
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Debug.Log("Quit button pressed");
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return $"{minutes:D2}:{seconds:D2}";
    }
}
