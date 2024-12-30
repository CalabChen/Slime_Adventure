using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    [Header("速度相关")]
    public float playerMoveSpeed;
    public float playerJumpSpeed;
    [Header("次数相关")]
    public int playerJumpCount;
    [Header("判断相关")]
    public bool isGround;
    public bool pressedJump;
    [Header("其他组件")]
    public Transform foot;
    public LayerMask Ground;
    public Rigidbody2D playerRB;
    public Collider2D playerColl;
    public Animator playerAnim;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerColl = GetComponent<Collider2D>();
        playerAnim = GetComponent<Animator>();

        playerMoveSpeed = 5f;
        playerJumpSpeed = 20f;
    }

    void Update()
    {
        UpdateCheck();
        AnimSwitch();
    }

    private void FixedUpdate()
    {
        FixedUpdateCheck();
        PlayerMove();
        PlayerJump();
    }

    void PlayerMove()
    {
        float horizontalNum = Input.GetAxis("Horizontal");
        float faceNum = Input.GetAxisRaw("Horizontal");
        playerRB.velocity = new Vector2(playerMoveSpeed * horizontalNum, playerRB.velocity.y);
        playerAnim.SetFloat("move", Mathf.Abs(playerMoveSpeed * horizontalNum));

        if (faceNum != 0)
        {
            transform.localScale = new Vector3(faceNum * 6f, transform.localScale.y, transform.localScale.z);
        }
    }

    void PlayerJump()
    {
        if (isGround)
        {
            playerJumpCount = 2;
        }
        if (pressedJump && isGround)
        {
            pressedJump = false;
            playerRB.velocity = new Vector2(playerRB.velocity.x, playerJumpSpeed);

            playerJumpCount--;

        }
        else if (pressedJump && playerJumpCount > 0 && !isGround)
        {
            pressedJump = false;
            playerRB.velocity = new Vector2(playerRB.velocity.x, playerJumpSpeed);

            playerJumpCount--;
        }
    }

    void FixedUpdateCheck()
    {
        isGround = Physics2D.OverlapCircle(foot.position, 0.1f, Ground);
    }

    void UpdateCheck()
    {

        if (Input.GetButtonDown("Jump"))
        {
            pressedJump = true;
        }
    }

    void AnimSwitch()
    {
        if (isGround)
        {
            playerAnim.SetBool("jump", false);
        }

        if (!isGround && playerRB.velocity.y != 0)
        {
            playerAnim.SetBool("jump", true);
        }
    }
}