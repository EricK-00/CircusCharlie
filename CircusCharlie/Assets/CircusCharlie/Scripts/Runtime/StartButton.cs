using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void OnStartButtonClick()
    {
        //SceneManager.LoadScene("01.Stage1");
        Debug.Log("click");
        StartCoroutine(GameManager.Instance.ClearStage());
    }
}
