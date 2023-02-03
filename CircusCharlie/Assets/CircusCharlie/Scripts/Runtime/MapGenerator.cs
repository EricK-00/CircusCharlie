using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject map;
    [SerializeField]
    private GameObject maps_Back;

    private void Awake()
    {
        switch (GameManager.Instance.Stage)
        {
            case 1:
                for (int i = 0; i < 10; i++)
                {
                    GameObject go = Instantiate(map, maps_Back.transform);
                    go.transform.localPosition = new Vector3(i * 1600, -90, 0);
                }
                break;
            default:
                for (int i = 0; i < 10; i++)
                {
                    GameObject go = Instantiate(map, maps_Back.transform);
                    go.transform.localPosition = new Vector3(i * 1600, -90, 0);
                }
                break;
        }
    }
}