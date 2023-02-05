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

    public bool GameOver { get; private set; } = false;
    public int Stage { get; private set; } = 1;
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
    [SerializeField]
    private GameObject scoreAdditionTextGO;
    private TMP_Text scoreAdditionText;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindGameManager().GetComponent<GameManager>();
                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    private static GameObject FindGameManager()
    {
        GameObject[] rootObjs_ = SceneManager.GetActiveScene().GetRootGameObjects();

        GameObject gameManager = default;
        foreach (GameObject rootObj in rootObjs_)
        {
            if (rootObj.name.Equals("GameManager"))
            {
                gameManager = rootObj;
                return gameManager;
            }
            else { continue; }
        }       // loop

        return gameManager;
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

    public IEnumerator ClearStage()
    {
        GameOver = true;
        Stage = Stage >= MAX_STAGE ? 1 : ++Stage;

        //시간 점수 획득


        PlayerPrefs.SetInt("HighScore", highScore);

        yield return new WaitForSecondsRealtime(2f);

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

        stageText.text = $"STAGE - {highScore.ToString().PadLeft(2, '0')}";
        stageStartUI.SetActive(true);
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
        scoreAdditionTextGO.transform.position = position;
        scoreAdditionTextGO.SetActive(true);
        scoreAdditionText.text = $"{addition}";
        yield return new WaitForSeconds(1f);
        scoreAdditionTextGO.SetActive(false);
    }

    public int GetMapSize(int stage)
    {
        switch (stage % 3)
        {
            case 0:
                return 6;
            case 2:
                return 7;
            default:
                return 2;
        }
    }
}