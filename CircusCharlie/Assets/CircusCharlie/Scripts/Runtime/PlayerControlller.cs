using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerControlller : MonoBehaviour
{
    private const float SPEED = 2f;
    
    [SerializeField]
    private GameObject backGround;
    [SerializeField]
    private GameObject additionPos;
    [SerializeField]
    private GameObject charlie;

    private Rigidbody2D rb;
    private bool isJumped = false;
    private int scrollDirection;
    private int accumulatedScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        charlie.GetComponent<Animator>().SetBool("isMove", false);

        if (isJumped)
        {
            backGround.transform.Translate(scrollDirection * SPEED * Time.deltaTime, 0, 0);
            return;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Left
            backGround.transform.Translate(1 * SPEED * Time.deltaTime, 0, 0);

            //Left + Jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb?.AddForce(Vector2.up * 350);
                isJumped = true;
                scrollDirection = 1;
                return;
            }

            charlie.GetComponent<Animator>().SetBool("isMove", true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //Right
            backGround.transform.Translate(-1 * SPEED * Time.deltaTime, 0, 0);

            //Right + Jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb?.AddForce(Vector2.up * 350);
                isJumped = true;
                scrollDirection = -1;
                return;
            }

            charlie.GetComponent<Animator>().SetBool("isMove", true);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            rb?.AddForce(Vector2.up * 350);
            isJumped = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            GameManager.Instance.AddScore(accumulatedScore);
            isJumped = false;
            scrollDirection = 0;
            accumulatedScore = 0;
            Debug.Log("착지 + 점수 계산");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacles")
        {
            Debug.Log("플레이어 사망 + 게임 멈추기");
        }
        else if (collision.gameObject.tag == "ScoreZone")
        {
            accumulatedScore += 100;
        }
        else if (collision.gameObject.tag == "Bag")
        {
            collision.gameObject.SetActive(false);
            accumulatedScore += 500;
            GameManager.Instance.AddScore(500);
            StartCoroutine(GameManager.Instance.ShowScoreAdded(500, additionPos.transform.position));
        }
    }

    private void Die()
    {

    }
}