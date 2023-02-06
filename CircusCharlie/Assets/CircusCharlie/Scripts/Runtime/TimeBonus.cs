using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeBonus : MonoBehaviour
{
    private const float waitTime = 0.5f;
    private const int MAX = 5000;
    public static int Bonus { get; private set; } = MAX;
    private TMP_Text timeBonusText;

    private void Awake()
    {
        timeBonusText = GetComponent<TMP_Text>();
        timeBonusText.text = $"BONUS - {Bonus.ToString().PadLeft(4, '0')}";
    }

    void Start()
    {
        StartCoroutine(DecreaseBonus());
    }

    private IEnumerator DecreaseBonus()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            if (Bonus > 0 && !GameManager.Instance.GameOver)
            {
                Bonus -= 10;
                timeBonusText.text = $"BONUS - {Bonus.ToString().PadLeft(4, '0')}";
            }
        }
    }

    public static void ResetTimeBonus()
    {
        Bonus = 5000;
    }
}
