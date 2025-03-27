using UnityEngine;
using UnityEngine.UI;
using TMPro;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player player;
    [SerializeField] private Spawner spawner;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private TMP_Text highScoreText;
    public int score { get; private set; } = 0;
    private int highScore = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            LoadHighScore();
        }
    }
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
    private void Start()
    {
        if (player == null) player = FindObjectOfType<Player>();
        if (spawner == null) spawner = FindObjectOfType<Spawner>();
        if (scoreText == null) scoreText = FindObjectOfType<TMP_Text>();
        if (highScoreText == null) highScoreText = GameObject.Find("HighScoreText")?.GetComponent<TMP_Text>();
        highScoreText.gameObject.SetActive(false);
        SoundPlayer.Instance.PlayGameMusic();
        Play();
    }
    public void Pause()
    {
        highScoreText.gameObject.SetActive(false);
        Time.timeScale = 0f;
        player.enabled = false;
    }
    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();
        playButton.SetActive(false);
        gameOver.SetActive(false);
        highScoreText.gameObject.SetActive(false);
        player.transform.position = Vector3.zero;
        Time.timeScale = 1f;
        player.enabled = true;
        Pipes[] pipes = FindObjectsOfType<Pipes>();
        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }
    public void GameOver()
    {
        if (score > highScore)
        {
            highScore = score;
            SaveHighScore();
            highScoreText.text = "High Score: " + highScore.ToString();
        }
        highScoreText.text = "High Score: " + highScore.ToString();
        highScoreText.gameObject.SetActive(true);
        Time.timeScale = 0f;
        player.enabled = false;
        playButton.SetActive(true);
        gameOver.SetActive(true);
    }
    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }
    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore.ToString();
    }
}