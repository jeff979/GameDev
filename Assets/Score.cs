using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score Instance;

    public int baseScore = 10;
    public int score = 0;
    public float multiplier = 1f;
    public float comboDecayRate = 0.1f;
    public float comboIncrease = 1f;
    public float maxMultiplier = 4f;

    public Text scoreText;
    public Text multiplierText;
    public Slider comboSlider;
    public Image comboFill;

    private Color hotColour = new Color(159f / 255f,0f,0f);
    private Color coldColour = new Color(255f / 255f, 162f / 255f, 0f);

    void Awake()
    {
        Instance = this;
    }

    //public void AddScore(int amount)
    //{
    //    score += amount;
    //    UpdateUI();
    //}

    //void UpdateUI()
    //{
    //    scoreText.text = "Score: " + score;
    //}

    void Update()
    {
        if (multiplier > 1f)
        {
            multiplier -= comboDecayRate * Time.deltaTime;
            multiplier = Mathf.Max(1f, multiplier);
            UpdateUI();
        }
    }

    public void AddKill()
    {
        multiplier += comboIncrease;
        multiplier = Mathf.Clamp(multiplier, 1f, maxMultiplier);

        int points = Mathf.RoundToInt(baseScore * multiplier);
        score += points;

        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "SCORE: " + score;

        if (multiplierText != null)
            multiplierText.text = "DAMAGE x" + multiplier.ToString("0.0");

        if (comboSlider != null)
            comboSlider.value = (multiplier - 1f) / (maxMultiplier - 1f);

        if (comboFill != null)
        {
            float ratio = (multiplier - 1f) / (maxMultiplier - 1f);
            comboFill.color = Color.Lerp(coldColour, hotColour, ratio);
        }
    }
}