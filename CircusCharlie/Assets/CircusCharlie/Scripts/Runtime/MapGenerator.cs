using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private const string OBJECT_CANVAS_NAME = "ObjectCanvas";
    private const string MAPS_NAME = "Maps";

    [SerializeField]
    private GameObject mapPrefab;
    [SerializeField]
    private GameObject maps;

    public void GenerateMap()
    {
        maps = Functions.GetRootGameObject(OBJECT_CANVAS_NAME).FindChildGameObject(MAPS_NAME);

        int stage = GameManager.Instance.Stage;
        int mapSize = GameManager.Instance.GetMapSize(stage);
        switch (stage)
        {
            case 1:
                for (int i = 0; i < mapSize - 1; i++)
                {
                    GameObject go = Instantiate(mapPrefab, maps.transform);
                    go.transform.localPosition = new Vector3(i * 1600, -90, 0);
                    go.GetComponentInChildren<TMP_Text>().text = $"{10 * mapSize - (i + 1) * 10}M";
                }
                break;
            default:
                for (int i = 0; i < mapSize - 1; i++)
                {
                    GameObject go = Instantiate(mapPrefab, maps.transform);
                    go.transform.localPosition = new Vector3(i * 1600, -90, 0);
                    go.GetComponentInChildren<TMP_Text>().text = $"{10 * mapSize - (i + 1) * 10}M";
                }
                break;
        }
    }
}