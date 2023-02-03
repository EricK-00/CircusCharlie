using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControlller : MonoBehaviour
{
    private const float SPEED = 3f;
    private const float JUMP_FORCE = 375;
    private const int MAP_COUNT = 2;
    private float mapWidth;
    private float playerStartX;
    private float mapStartX;
    private float bgScrollingEndX;
    
    [SerializeField]
    private GameObject[] map = new GameObject[2];
    [SerializeField]
    private GameObject scoreAdditionPos;
    [SerializeField]
    private Animator charlieAnim;
    [SerializeField]
    private Animator lionAnim;

    private Rigidbody2D rb;
    private bool isJumped = false;
    private int scrollDirection;
    private int accumulatedScore = 0;

    private bool playerMoveMode = false;
    private bool isLeftLocked = false;
    private bool isRightLocked = false;

    private enum Direction
    {
        Left = -1,
        Right = 1,
    }

    private void Awake()
    {
        mapWidth = map[0].GetComponent<RectTransform>().rect.width;
        playerStartX = transform.localPosition.x;
        mapStartX = map[0].transform.localPosition.x - playerStartX;
        bgScrollingEndX = mapStartX - mapWidth * (MAP_COUNT - 1);

        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        charlieAnim.SetBool("isMove", false);
        lionAnim.SetBool("isMove", false);

        if (bgScrollingEndX - playerStartX >= map[0].transform.localPosition.x - transform.localPosition.x)
        {
            playerMoveMode = true;
        }
        else
        {
            transform.localPosition = new Vector2 (-400, transform.localPosition.y);
            playerMoveMode = false;
        }

        if (isJumped)
        {
            if ((scrollDirection == -1 && isLeftLocked) ||
                (scrollDirection == 1 && isRightLocked))
            {
                return;
            }

            if (playerMoveMode)
            {
                Move(scrollDirection, gameObject);
            }
            else
            {
                Move(-scrollDirection, map);
            }
            return;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Left + Jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump(-1);
                return;
            }

            //Left
            //이동 제한
            if (isLeftLocked)
            {
                return;
            }
            if (playerMoveMode)
            {
                Move(-1, gameObject);
            }
            else
            {
                Move(1, map);
            }

            charlieAnim.SetBool("isMove", true);
            lionAnim.SetBool("isMove", true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //Right + Jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump(1);
                return;
            }

            //Right
            //이동 제한
            if (isRightLocked)
            {
                return;
            }
            if (playerMoveMode)
            {
                Move(1, gameObject);
            }
            else
            {
                Move(-1, map);
            }

            charlieAnim.SetBool("isMove", true);
            lionAnim.SetBool("isMove", true);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump(0);
        }
    }

    private void Move(int moveDirection, params GameObject[] targets)
    {
        foreach (var go in targets)
        {
            go.transform.Translate(moveDirection * SPEED * Time.deltaTime, 0, 0);
        }
    }

    private void Jump(int moveDirection)
    {
        if (!isJumped)
        {
            isJumped = true;
            scrollDirection = moveDirection;
            rb?.AddForce(Vector2.up * JUMP_FORCE);
            charlieAnim.SetBool("isMove", false);
            lionAnim.SetBool("isJump", true);
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
            lionAnim.SetBool("isJump", false);
            Debug.Log("착지 + 점수 계산");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacles")
        {
            Die();
        }
        else if (other.gameObject.tag == "ScoreZone")
        {
            accumulatedScore += 100;
        }
        else if (other.gameObject.tag == "Bag")
        {
            other.gameObject.SetActive(false);
            accumulatedScore += 500;
            GameManager.Instance.AddScore(500);
            StartCoroutine(GameManager.Instance.ShowScoreAdded(500, scoreAdditionPos.transform.position));
        }
        else if (other.gameObject.tag == "LeftWall")
        {
            isLeftLocked = true;
        }
        else if (other.gameObject.tag == "RightWall")
        {
            isRightLocked = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("!");
        if (other.gameObject.tag == "LeftWall")
        {
            Debug.Log("a");
            isLeftLocked = false;
        }
        else if (other.gameObject.tag == "RightWall")
        {
            Debug.Log("b");
            isRightLocked = false;
        }
    }

    private void Die()
    {
        Debug.Log("플레이어 사망 + 게임 멈추기");
    }
}