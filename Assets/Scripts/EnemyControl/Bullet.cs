using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // 子弹速度
    public int damage = 10;   // 子弹伤害
    public Vector2 direction; // 子弹方向

    private Rigidbody2D rb;
    private float lifeTime = 5f; // 子弹生命周期（5 秒）
    private float spawnTime;     // 子弹生成时间

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // 禁用重力
        rb.velocity = new Vector2(direction.x * speed, 0); // 设置子弹速度，Y 轴速度为 0

        spawnTime = Time.time; // 记录子弹生成时间
    }

    void Update()
    {
        // 检查子弹是否超过生命周期
        if (Time.time - spawnTime > lifeTime)
        {
            Destroy(gameObject); // 销毁子弹
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检测碰撞对象是否是玩家
        if (collision.gameObject.CompareTag("Player"))
        {
            // 触发玩家的 Die 函数
            SlimeController player = collision.gameObject.GetComponent<SlimeController>();
            if (player != null)
            {
                player.Die();
            }
            Destroy(gameObject); // 销毁子弹
        }
        // 检测碰撞对象是否是地面（通过 Layer 判断）
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject); // 碰到地面时销毁子弹
        }
    }
}