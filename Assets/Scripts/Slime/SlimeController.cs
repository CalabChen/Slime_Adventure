using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlimeController : MonoBehaviour
{
    [Header("速度设置")]
    public float playerMoveSpeed; // 玩家移动速度
    public float playerJumpSpeed; // 玩家跳跃速度
    public float dashingPower;
    public float dashingTime;
    public float dashingCoolDown;
    public float horizontal;

    [Header("跳跃设置")]
    public int playerJumpCount;   // 玩家可跳跃次数

    [Header("检测变量")]
    public bool isGround;        // 是否着地
    public bool pressedJump;     // 是否按下跳跃键
    public bool canDash;
    public bool isDashing;


    [Header("组件")]
    public Transform foot;       // 用于检测地面的脚部位置
    public LayerMask groundLayer;// 地面图层遮罩
    public Rigidbody2D playerRB; // 玩家刚体组件
    public Collider2D playerColl;// 玩家碰撞器组件
    public Animator playerAnim;// 玩家动画控制器
    private SpriteRenderer spriteRenderer;
    public CinemachineVirtualCamera vcam;
    public TrailRenderer playerTr;
    public AudioSource jumpSoundEffect;
    public AudioSource dashSoundEffect;
    public AudioSource deathSoundEffect;

    [Header("重生点")]
    public Transform respawnPoint;

    public static SlimeController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // 在Awake时设置实例
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // 确保只有一个玩家实例
        }
    }

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerColl = GetComponent<Collider2D>();
        playerAnim = GetComponent<Animator>();
        playerTr = GetComponent<TrailRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 默认速度设置
        playerMoveSpeed = 5f;
        playerJumpSpeed = 15f;

        dashingPower = 15f;
        dashingTime = 0.4f;
        dashingCoolDown = 0.5f;
        canDash = true;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (isDashing)
        {
            return;
        }

        UpdateCheck(); // 更新按键检测
        AnimSwitch();  // 切换动画状态        

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            dashSoundEffect.Play();
            StartCoroutine(Dash());
        }
    }
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
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
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (faceDirection != 0)
        { spriteRenderer.flipX = (faceDirection < 0);
            // 根据移动方向翻转角色
            // transform.localScale = new Vector3(faceDirection * 6f, transform.localScale.y, transform.localScale.z);
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
            jumpSoundEffect.Play();
            playerRB.velocity = new Vector2(playerRB.velocity.x, playerJumpSpeed);
            playerJumpCount--;
        }
        else if (pressedJump && playerJumpCount > 0 && !isGround)
        {
            // 在空中进行额外跳跃（如果还有可用跳跃次数）
            pressedJump = false;
            jumpSoundEffect.Play();
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

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = playerRB.gravityScale;
        playerRB.gravityScale = 0f;
        int dashDirection = spriteRenderer.flipX ? -1 : 1;
        playerRB.velocity = new Vector2(dashDirection * dashingPower, 0f);
        playerTr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        playerTr.emitting = false;
        playerRB.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCoolDown);
        canDash = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("DeathFall") || collision.gameObject.CompareTag("Bullet"))
        {   
            Die();
        }
    }

    public void Die()
    {
        deathSoundEffect.Play();
        playerAnim.SetTrigger("death");
        playerRB.bodyType = RigidbodyType2D.Static;
        Respawn();
    }

    private void Respawn()
    {
        StartCoroutine(DelayedRespawn());
    }
    private IEnumerator DelayedRespawn()
    {
        // 等待死亡动画完成
        yield return new WaitForSeconds(playerAnim.GetCurrentAnimatorStateInfo(0).length);
        // 重置玩家状态
        playerAnim.ResetTrigger("death");
        playerRB.bodyType = RigidbodyType2D.Dynamic;
        transform.position = respawnPoint.position;
        RestartLevel();
        // 重新设置Cinemachine摄像头的Follow和LookAt
        vcam.Follow = transform;
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}