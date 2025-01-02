using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskDude : MonoBehaviour
{
    [Header("速度设置")]
    public float enemyRunSpeed;

    [Header("检测变量")]
    public bool isGround;        // 是否着地
    public bool _collided;

    [Header("攻击设置")]
    public float attackRange = 10f; // 攻击范围
    public float attackCooldown = 2.0f; // 攻击冷却时间
    public GameObject bulletPrefab; // 子弹预制体
    private float lastAttackTime = 0f; // 上次攻击时间

    [Header("组件")]
    public Transform foot;
    public LayerMask groundLayer;
    public BoxCollider2D headCollider; // 使用 BoxCollider2D 作为 headPoint
    public Transform rightUp;
    public Transform rightDown;
    public Rigidbody2D enemyRB;
    public CapsuleCollider2D enemyCapColl;
    public Animator enemyAnim;

    private bool isDead = false; // 标志变量，避免重复触发死亡逻辑

    // Start is called before the first frame update
    void Start()
    {
        enemyRunSpeed = 2.0f;

        enemyRB = GetComponent<Rigidbody2D>();
        enemyCapColl = GetComponent<CapsuleCollider2D>();
        enemyAnim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // 移动 MaskDude
        Vector3 movement = new Vector3(enemyRunSpeed, enemyRB.velocity.y, 0);
        transform.position += movement * Time.deltaTime;

        // 检测前方是否有障碍物
        _collided = Physics2D.Linecast(rightUp.position, rightDown.position, groundLayer);

        // 绘制调试线
        if (_collided)
        {
            Debug.DrawLine(rightUp.position, rightDown.position, Color.red); // 碰撞时显示红色线
        }
        else
        {
            Debug.DrawLine(rightUp.position, rightDown.position, Color.green); // 未碰撞时显示绿色线
        }

        // 如果检测到碰撞，翻转 MaskDude 的朝向
        if (_collided)
        {
            Flip();
        }

        // 检测是否在地面上
        FixedUpdateCheck();

        // 检测玩家并发射子弹
        CheckForPlayerAndAttack();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检测 Slime 是否踩到 headPoint
        if (other.CompareTag("Player") && !isDead)
        {
            // 触发 NijiaFrog 死亡逻辑
            isDead = true; // 设置标志变量，避免重复触发

            // 给 Slime 一个向上的力，确保它跳起来
            Rigidbody2D slimeRB = other.GetComponent<Rigidbody2D>();
            if (slimeRB != null)
            {
                slimeRB.velocity = new Vector2(slimeRB.velocity.x, 0); // 重置 y 轴速度
                slimeRB.AddForce(Vector2.up * 12.0f, ForceMode2D.Impulse); // 给一个较大的向上的力
            }

            // 禁用碰撞器，避免 OnCollisionEnter2D 被触发
            //enemyCapColl.enabled = false;

            // 触发 NijiaFrog 死亡动画和销毁逻辑
            enemyRunSpeed = 0f;
            enemyAnim.SetTrigger("die");
            //Destroy(gameObject, 1f);
            Destroy(gameObject, 0.5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isDead)
        {
            SlimeController slime = collision.gameObject.GetComponent<SlimeController>();
            if (slime != null)
            {
                slime.Die(); // 调用 Die 方法，而不是 Respawn
            }
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            // 改变移动方向
            Flip();
        }
    }

    void FixedUpdateCheck()
    {
        // 检测角色是否在地上
        isGround = Physics2D.OverlapCircle(foot.position, 0.1f, groundLayer);
    }

    // 翻转 MaskDude 的朝向
    private void Flip()
    {
        // 反转移动方向
        enemyRunSpeed *= -1;
        // 翻转 MaskDude 的朝向
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // 检测玩家并发射子弹
    private void CheckForPlayerAndAttack()
    {
        // 检测玩家是否在攻击范围内
        Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D player in players)
        {
            if (player.CompareTag("Player") && Time.time > lastAttackTime + attackCooldown)
            {
                // 发射子弹
                Shoot();

                // 记录上次攻击时间
                lastAttackTime = Time.time;
                break; // 只攻击一个玩家
            }
        }
    }

    // 发射子弹
    private void Shoot()
    {
        if (bulletPrefab && rightUp && rightDown)
        {
            // 选择发射方向（例如使用 rightUp）
            Vector2 spawnPosition = rightUp.position;
            Vector2 direction = (rightUp.position - transform.position).normalized;

            // 创建子弹
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

            // 根据 MaskDude 的朝向调整子弹的旋转
            if (transform.localScale.x > 0) // 如果 MaskDude 朝左
            {
                bullet.transform.Rotate(0, -180, 0); // 旋转 180 度
            }

            // 设置子弹方向
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript)
            {
                bulletScript.direction = direction;
            }
        }
    }

    // 绘制攻击范围
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}