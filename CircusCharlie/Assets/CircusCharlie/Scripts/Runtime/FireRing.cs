using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRing : MonoBehaviour
{
    private float speed = 1f;

    void Update()
    {
        transform.Translate(Vector2.left * speed  * Time.deltaTime);
    }
}