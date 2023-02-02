using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance 
    {
        get
        {
            if (instance == null)
                instance = GameObject.Find("GameManager").GetComponent<GameManager>();

            return instance; 
        }
    }

    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private GameObject additionTextGO;
    private TMP_Text additionText;

    private static int score = 0;

    private void Awake()
    {
        additionText = additionTextGO.GetComponent<TMP_Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int addition)
    {
        score += addition;
        UpdateHighScore();
        scoreText.text = $"SCORE - {score.ToString().PadLeft(6, '0')}";
    }

    private void UpdateHighScore()
    {

    }

    public IEnumerator ShowScoreAdded(int addition, Vector2 position)
    {
        additionTextGO.transform.position = position;
        additionTextGO.SetActive(true);
        additionText.text = $"{addition}";
        yield return new WaitForSeconds(1f);
        additionTextGO.SetActive(false);
    }
}