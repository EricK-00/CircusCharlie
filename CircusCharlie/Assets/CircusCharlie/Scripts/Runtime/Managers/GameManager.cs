using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private readonly string[] SCENE_NAMES = { "01. Stage1", "02. Stage2", "03. Stage3" };
    private const int MAX_SCORE = 999999;
    private const int MAX_STAGE = 99;

    public bool GameOver { get; private set; } = true;
    public int Stage { get; private set; } = 0;
    private int score = 0;
    private int highScore = 0;

    [SerializeField]
    private GameObject stageStartUI;
    [SerializeField]
    private TMP_Text stageText;
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private TMP_Text highScoreText;

    private const string OBJECT_CANVAS_NAME = "ObjectCanvas";
    private const string SCORE_ADDITION_TEXT_NAME = "ScoreAdditionText";
    private TMP_Text scoreAdditionText;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Functions.GetRootGameObject("GameManager").GetComponent<GameManager>();
                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    private void Awake()
    {
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

    public IEnumerator ClearStage()
    {
        GameOver = true;

        //시간 점수 획득
        if (Stage > 0)
        {
            AddScore(TimeBonus.Bonus);
            TimeBonus.ResetTimeBonus();
        }

        PlayerPrefs.SetInt("HighScore", highScore);
        Stage = Stage >= MAX_STAGE ? 1 : ++Stage;

        yield return new WaitForSecondsRealtime(1f);

        switch (Stage % 3)
        {
            case 1:
                SceneManager.LoadScene(SCENE_NAMES[0]);
                break;
            case 2:
                SceneManager.LoadScene(SCENE_NAMES[1]);
                break;
            default:
                SceneManager.LoadScene(SCENE_NAMES[2]);
                break;
        }
        stageText.text = $"STAGE - {Stage.ToString().PadLeft(2, '0')}";
        stageStartUI.SetActive(true);
    }

    public void StartNewStage()
    {
        scoreAdditionText = Functions.GetRootGameObject(OBJECT_CANVAS_NAME).FindChildGameObject(SCORE_ADDITION_TEXT_NAME).GetComponent<TMP_Text>();
        GetComponent<MapGenerator>().GenerateMap();
        GameOver = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ++Stage;
            SceneManager.LoadScene(SCENE_NAMES[Stage % 3]);
        }
    }

    public IEnumerator ShowScoreAdded(int addition, Vector2 position)
    {
        scoreAdditionText.transform.position = position;
        scoreAdditionText.gameObject.SetActive(true);
        scoreAdditionText.text = $"{addition}";
        yield return new WaitForSeconds(1f);
        scoreAdditionText.gameObject.SetActive(false);
    }

    public int GetMapSize(int stage)
    {
        switch (stage % 3)
        {
            case 1:
                return 10;
            case 2:
                return 10;
            default:
                return 10;
        }
    }
}