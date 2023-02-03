using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private const int MAX_SCORE = 999999;
    private const int MAX_STAGE = 99;

    public int Stage { get; private set; } = 1;
    private int score = 0;
    private int highScore = 0;

    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private TMP_Text highScoreText;
    [SerializeField]
    private GameObject scoreAdditionTextGO;
    private TMP_Text scoreAdditionText;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find("GameManager").GetComponent<GameManager>();
                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    private void Awake()
    {
        scoreAdditionText = scoreAdditionTextGO.GetComponent<TMP_Text>();
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
            highScoreText.text = $"HI - {highScore.ToString().PadLeft(6, '0')}";
        }
    }

    public void AddScore(int addition)
    {
        if (score < MAX_SCORE)
        {
            score += addition;
            UpdateHighScore();
            scoreText.text = $"SCORE - {score.ToString().PadLeft(6, '0')}";
        }
    }

    private void UpdateHighScore()
    {
        if (highScore < score)
        {
            Debug.Log(highScore);
            highScore = score;
            highScoreText.text = $"HI - {highScore.ToString().PadLeft(6, '0')}";
        }
    }

    private void ClearStage()
    {
        Stage = Stage >= MAX_STAGE ? 1 : ++Stage;
        highScoreText.text = $"STAGE - {highScore.ToString().PadLeft(2, '0')}";
        PlayerPrefs.SetInt("HighScore", highScore);
        Debug.Log("점수 계산");
    }

    public IEnumerator ShowScoreAdded(int addition, Vector2 position)
    {
        scoreAdditionTextGO.transform.position = position;
        scoreAdditionTextGO.SetActive(true);
        scoreAdditionText.text = $"{addition}";
        yield return new WaitForSeconds(1f);
        scoreAdditionTextGO.SetActive(false);
    }
}