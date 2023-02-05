using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeBonus : MonoBehaviour
{
    private const float waitTime = 0.5f;
    private const int MAX = 5000;
    public int bonus = MAX;
    private TMP_Text timeBonusText;

    private void Awake()
    {
        timeBonusText = GetComponent<TMP_Text>();
        timeBonusText.text = $"BONUS - {bonus.ToString().PadLeft(4, '0')}";
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

            if (bonus > 0)
            {
                bonus -= 10;
                timeBonusText.text = $"BONUS - {bonus.ToString().PadLeft(4, '0')}";
            }
        }
    }
}
