using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void SetCamera()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }
}
