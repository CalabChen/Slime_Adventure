using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    [Header("速度设置")]
    public float playerMoveSpeed; // 玩家移动速度
    public float playerJumpSpeed; // 玩家跳跃速度

    [Header("跳跃设置")]
    public int playerJumpCount;   // 玩家可跳跃次数

    [Header("检测变量")]
    public bool isGround;        // 是否着地
    public bool pressedJump;     // 是否按下跳跃键

    [Header("组件")]
    public Transform foot;       // 用于检测地面的脚部位置
    public LayerMask groundLayer;// 地面图层遮罩
    public Rigidbody2D playerRB; // 玩家刚体组件
    public Collider2D playerColl;// 玩家碰撞器组件
    public Animator playerAnim;  // 玩家动画控制器

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerColl = GetComponent<Collider2D>();
        playerAnim = GetComponent<Animator>();

        // 默认速度设置
        playerMoveSpeed = 5f;
        playerJumpSpeed = 15f;
    }

    void Update()
    {
        UpdateCheck(); // 更新按键检测
        AnimSwitch();  // 切换动画状态
    }

    private void FixedUpdate()
    {
        FixedUpdateCheck(); // 固定更新检测
        PlayerMove();       // 玩家移动逻辑
        PlayerJump();       // 玩家跳跃逻辑
    }

    void PlayerMove()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float faceDirection = Input.GetAxisRaw("Horizontal");

        // 应用水平移动
        playerRB.velocity = new Vector2(playerMoveSpeed * horizontalInput, playerRB.velocity.y);

        // 将移动值传递给动画控制器
        playerAnim.SetFloat("move", Mathf.Abs(playerMoveSpeed * horizontalInput));

        if (faceDirection != 0)
        {
            // 根据移动方向翻转角色
            transform.localScale = new Vector3(faceDirection * 6f, transform.localScale.y, transform.localScale.z);
        }
    }

    void PlayerJump()
    {
        if (isGround)
        {
            // 当接触地面时重置跳跃次数
            playerJumpCount = 2;
        }

        if (pressedJump && isGround)
        {
            // 当在地面上且按下跳跃键时执行跳跃
            pressedJump = false;
            playerRB.velocity = new Vector2(playerRB.velocity.x, playerJumpSpeed);
            playerJumpCount--;
        }
        else if (pressedJump && playerJumpCount > 0 && !isGround)
        {
            // 在空中进行额外跳跃（如果还有可用跳跃次数）
            pressedJump = false;
            playerRB.velocity = new Vector2(playerRB.velocity.x, playerJumpSpeed);
            playerJumpCount--;
        }
    }

    void FixedUpdateCheck()
    {
        // 检测角色是否在地上
        isGround = Physics2D.OverlapCircle(foot.position, 0.1f, groundLayer);
    }

    void UpdateCheck()
    {
        // 当按下跳跃键时设置跳跃标志
        if (Input.GetButtonDown("Jump"))
        {
            pressedJump = true;
        }
    }

    void AnimSwitch()
    {
        // 根据角色是否跳跃切换动画状态
        playerAnim.SetBool("jump", !isGround && playerRB.velocity.y != 0);
    }
}