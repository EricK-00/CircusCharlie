using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;
    public bool IsMounted { get; private set; } = false;
    private int moveDirection = -1;
    private Animator anim;

    private UnityEvent dismountEvent = new UnityEvent();

    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isMounted", IsMounted);
    }

    private void Update()
    {
        if (!IsMounted)
        {
            transform.Translate(new Vector2(moveDirection, 0) * speed * Time.deltaTime);
        }
    }

    public void Mounted(PlayerControlller player)
    {
        IsMounted = true;
        anim.SetBool("isMounted", IsMounted);
        dismountEvent.AddListener(player.DismountBall);
    }

    public void Dismounted(int direction)
    {
        IsMounted = false;
        anim.SetBool("isMounted", IsMounted);
        speed = 8f;
        moveDirection = direction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsMounted && other.tag == "Ball")
        {
            dismountEvent.Invoke();
            other.GetComponent<Ball>().Dismounted(-moveDirection);

            dismountEvent.RemoveAllListeners();
            if (moveDirection == 0)
            {
                moveDirection = -1;
            }
        }
    }
}
