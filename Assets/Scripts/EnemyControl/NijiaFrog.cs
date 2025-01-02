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
            enemyCapColl.enabled = false;

            // 触发 NijiaFrog 死亡动画和销毁逻辑
            enemyRunSpeed = 0f;
            enemyAnim.SetTrigger("die");
            Destroy(gameObject, 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 检测 Slime 是否碰撞到 NijiaFrog 的其他部位
        if (collision.gameObject.CompareTag("Player") && !isDead)
        {
            // 触发 Slime 掉血逻辑
            SlimeController slime = collision.gameObject.GetComponent<SlimeController>();
            if (slime != null)
            {
                slime.Die(); // 调用 Die 方法，而不是 Respawn
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