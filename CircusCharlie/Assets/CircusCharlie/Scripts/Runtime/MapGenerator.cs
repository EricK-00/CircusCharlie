using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject mapPrefab;
    [SerializeField]
    private GameObject maps;

    private void Awake()
    {
        int stage = GameManager.Instance.Stage;
        int mapSize = GameManager.Instance.GetMapSize(stage);
        switch (stage)
        {
            case 1:
                for (int i = 0; i < mapSize; i++)
                {
                    GameObject go = Instantiate(mapPrefab, maps.transform);
                    go.transform.localPosition = new Vector3(i * 1600, -90, 0);
                    go.GetComponentInChildren<TMP_Text>().text = $"{10 * mapSize - (i + 1) * 10}M";
                }
                break;
            default:
                for (int i = 0; i < GameManager.Instance.GetMapSize(1); i++)
                {
                    GameObject go = Instantiate(mapPrefab, maps.transform);
                    go.transform.localPosition = new Vector3(i * 1600, -90, 0);
                    go.GetComponentInChildren<TMP_Text>().text = $"{10 * mapSize - (i + 1) * 10}M";
                }
                break;
        }
    }
}