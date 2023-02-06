using System.Collections;
using TMPro;
using UnityEngine;

public class StageStartUI : MonoBehaviour
{
    private TMP_Text stageTextInStartUI;

    private void Awake()
    {
        stageTextInStartUI = transform.GetChild(0).GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        stageTextInStartUI.text = $"STAGE {GameManager.Instance.Stage.ToString().PadLeft(2, '0')}";
        StartCoroutine(ShowStageStageUI());
    }

    IEnumerator ShowStageStageUI()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
        GameManager.Instance.StartNewStage();
        gameObject.SetActive(false);
    }
}