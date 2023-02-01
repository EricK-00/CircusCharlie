using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpButton : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D playerRb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnJumpButton()
    {
        playerRb?.AddForce(Vector2.up * 300);
    }
}