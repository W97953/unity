using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Score")]
    public int score = 0;
    public Text scoreText; // replace with TextMeshProUGUI if desired

    [Header("Win")]
    public GameObject winPanel;

    void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); }
    }

    void Start()
    {
        UpdateUI();
        if (winPanel) winPanel.SetActive(false);
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText) scoreText.text = "Score: " + score;
    }

    public void Win()
    {
        if (winPanel) winPanel.SetActive(true);
        Time.timeScale = 0f; // pause
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
