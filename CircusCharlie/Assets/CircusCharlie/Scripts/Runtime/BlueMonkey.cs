using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueMonkey : MonoBehaviour
{
    private float speed = 1f;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //점프
    }
}
