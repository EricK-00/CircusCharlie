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
    private float mapWidth;
    private float playerStartX;
    private float mapStartX;
    private float bgScrollingEndX;

    [SerializeField]
    private GameObject[] backgrounds;
    [SerializeField]
    private GameObject[] characters;
    [SerializeField]
    private GameObject scoreAdditionPos;
    [SerializeField]
    private Animator charlieAnim;
    [SerializeField]
    private Animator mountAnim;
    [SerializeField]
    private RectTransform playerRect;

    private Rigidbody2D playerRb;
    private RectTransform ballRect;

    private bool isJumped = false;
    private int moveDirection;
    private int accumulatedScore = 0;

    private bool playerMoveMode = false;
    private bool isLeftLocked = false;
    private bool isRightLocked = false;

    private bool isAdditionAdded  = false;
    private float ballYPos = -64f;
    private int ballCounter = 0;

    private void Awake()
    {
        mapWidth = backgrounds[0].GetComponent<RectTransform>().rect.width;
        playerStartX = transform.localPosition.x;
        mapStartX = backgrounds[0].transform.localPosition.x - playerStartX;
        bgScrollingEndX = mapStartX - mapWidth * (GameManager.Instance.GetMapSize(GameManager.Instance.Stage));

        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        charlieAnim.SetBool("isMove", false);
        if (mountAnim != null)
        mountAnim.SetBool("isMove", false);

        playerMoveMode = (bgScrollingEndX - playerStartX >= backgrounds[0].transform.localPosition.x - transform.localPosition.x) ? true : false;

        if (isJumped)
        {
            if ((moveDirection == -1 && isLeftLocked) ||
                (moveDirection == 1 && isRightLocked))
            {
                return;
            }
            Move(moveDirection);
            return;
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Joystick.GetKeyLeft())
        {
            //Left + Jump
            if (Input.GetKeyDown(KeyCode.Space) || JumpButton.GetKeyJump())
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
            Move(-1);

            charlieAnim.SetBool("isMove", true);
            if (mountAnim != null)
                mountAnim.SetBool("isMove", true);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Joystick.GetKeyRight())
        {
            //Right + Jump
            if (Input.GetKeyDown(KeyCode.Space) || JumpButton.GetKeyJump())
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
            Move(1);

            charlieAnim.SetBool("isMove", true);
            if (mountAnim != null)
                mountAnim.SetBool("isMove", true);
        }
        else if (Input.GetKeyDown(KeyCode.Space) || JumpButton.GetKeyJump())
        {
            Jump(0);
        }
        else
        {
            moveDirection = 0;
        }
    }

    private void Move(int direction)
    {
        moveDirection = direction;
        GameObject[] targets = playerMoveMode ? characters : backgrounds;
        direction = playerMoveMode ? direction : -direction;

        foreach (var go in targets)
        {
            if (go != null)
                go.transform.Translate(direction * SPEED * Time.deltaTime, 0, 0);
        }
    }

    private void Jump(int direction)
    {
        if (!isJumped)
        {
            isJumped = true;
            moveDirection = direction;
            playerRb?.AddForce(Vector2.up * JUMP_FORCE);
            charlieAnim.SetBool("isMove", false);
            charlieAnim.SetBool("isJump", true);
            if (mountAnim != null)
            {
                if (mountAnim.name == "Lion")
                {
                    mountAnim.SetBool("isJump", true);
                }
                else if (mountAnim.name == "Ball")
                {
                    if (direction == 0)
                        return;

                    //DismountBall(moveDirection);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            AddScore();
            Land();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Obstacles")
        {
            Die();
        }
        else if (other.tag == "ScoreZone")
        {
            accumulatedScore += 100;

            if (other.transform.parent.tag == "Ball")
            {
                ++ballCounter;
                if (ballCounter == 2)
                {
                    isAdditionAdded = true;
                    accumulatedScore -= 100;
                }
            }

        }
        else if (other.tag == "SpecialScoreZone")
        {
            if (other.name == "Bag")
            {
                other.gameObject.SetActive(false);
                GameManager.Instance.AddScore(500);
                StartCoroutine(GameManager.Instance.ShowScoreAdded(500, scoreAdditionPos.transform.position));
            }
            else
            {
                isAdditionAdded = true;
            }
        }
        else if (other.tag == "Floor" && playerRb.velocity.y <= 0)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
            playerRb.gravityScale = 0f;

            AddScore();
            Land();

            if (other.name == "GoalFloor")
            {
                StartCoroutine(GameManager.Instance.ClearStage());
            }
            else if (other.name == "BallFloor")
            {
                MountBall(other.gameObject);
            }
        }
        else if (other.tag == "LeftWall")
        {
            isLeftLocked = true;
        }
        else if (other.tag == "RightWall")
        {
            isRightLocked = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "LeftWall")
        {
            isLeftLocked = false;
        }
        else if (other.tag == "RightWall")
        {
            isRightLocked = false;
        }
        else if (other.tag == "Floor" && other.name == "BallFloor")
        {
            isJumped = true;
            playerRb.gravityScale = 1f;

            //제자리 점프
            if (moveDirection == 0)
                return;

            DismountBall();
        }
    }

    private void AddScore()
    {
        if (isAdditionAdded)
        {
            accumulatedScore += 500;
            StartCoroutine(GameManager.Instance.ShowScoreAdded(500, scoreAdditionPos.transform.position));
            isAdditionAdded = false;
        }

        GameManager.Instance.AddScore(accumulatedScore);
        accumulatedScore = 0;
        ballCounter = 0;
    }

    private void Land()
    {
        isJumped = false;
        moveDirection = 0;
        charlieAnim.SetBool("isJump", false);
        if (mountAnim != null && mountAnim.name == "Lion")
            mountAnim.SetBool("isJump", false);
    }

    private void Die()
    {
        Debug.Log("플레이어 사망 + 게임 멈추기");
    }

    private void MountBall(GameObject ball)
    {
        if (characters[1] == null && mountAnim == null)
        {
            characters[1] = ball.transform.parent.gameObject;
            ballRect = characters[1].GetComponent<RectTransform>();
            ballRect.SetParent(playerRect);
            ballRect.localPosition = new Vector2(characters[0].transform.localPosition.x, ballYPos);
            characters[1].GetComponent<Ball>().Mounted(this);
            mountAnim = characters[1].GetComponent<Animator>();
        }
    }

    public void DismountBall()
    {
        if (characters[1] != null && mountAnim != null)
        {
            ballRect.SetParent(backgrounds[1].GetComponent<RectTransform>());
            characters[1].GetComponent<Ball>().Dismounted(-moveDirection);
            mountAnim = null;
            characters[1] = null;
        }
    }
}