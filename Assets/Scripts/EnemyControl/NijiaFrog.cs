using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NijiaFrog : MonoBehaviour
{
    [Header("速度设置")]
    public float enemyRunSpeed;

    [Header("检测变量")]
    public bool isGround;        // 是否着地
    public bool _collided;

    [Header("组件")]
    public Transform foot;
    public LayerMask groundLayer;
    public Transform headPoint;
    public Transform rightUp;
    public Transform rightDown;
    public Rigidbody2D enemyRB;
    public CapsuleCollider2D enemyCapColl;
    public Animator enemyAnim;


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
        // 移动 NijiaFrog
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

        // 如果检测到碰撞，翻转 NijiaFrog 的朝向
        if (_collided)
        {
            // 翻转 NijiaFrog 的朝向
            Flip();
        }

        // 检测是否在地面上
        FixedUpdateCheck();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            float height = collision.contacts[0].point.y - headPoint.position.y;

            if (height > 0)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 8.0f, ForceMode2D.Impulse);
                enemyRunSpeed = 0f;
                enemyAnim.SetTrigger("die");
                Destroy(gameObject, 1f);
            }
        }
    }

    void FixedUpdateCheck()
    {
        // 检测角色是否在地上
        isGround = Physics2D.OverlapCircle(foot.position, 0.1f, groundLayer);
    }

    // 翻转 NijiaFrog 的朝向
    private void Flip()
    {
        // 反转移动方向
        enemyRunSpeed *= -1;
        // 翻转 NijiaFrog 的朝向
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}