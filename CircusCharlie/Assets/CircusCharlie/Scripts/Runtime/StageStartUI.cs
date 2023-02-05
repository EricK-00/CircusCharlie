using System.Collections;
using TMPro;
using UnityEngine;

public class StageStartUI : MonoBehaviour
{
    [SerializeField]
    TMP_Text stageText;

    private void Awake()
    {
        stageText.text = $"STAGE {GameManager.Instance.Stage.ToString().PadLeft(2, '0')}";
    }

    private void Start()
    {
        StartCoroutine(ShowStageStageUI());
    }

    IEnumerator ShowStageStageUI()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}