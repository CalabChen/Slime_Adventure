using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // �ӵ��ٶ�
    public int damage = 10;   // �ӵ��˺�
    public Vector2 direction; // �ӵ�����

    private Rigidbody2D rb;
    private float lifeTime = 5f; // �ӵ��������ڣ�5 �룩
    private float spawnTime;     // �ӵ�����ʱ��

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // ��������
        rb.velocity = new Vector2(direction.x * speed, 0); // �����ӵ��ٶȣ�Y ���ٶ�Ϊ 0

        spawnTime = Time.time; // ��¼�ӵ�����ʱ��
    }

    void Update()
    {
        // ����ӵ��Ƿ񳬹���������
        if (Time.time - spawnTime > lifeTime)
        {
            Destroy(gameObject); // �����ӵ�
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �����ײ�����Ƿ������
        if (collision.gameObject.CompareTag("Player"))
        {
            // ������ҵ� Die ����
            SlimeController player = collision.gameObject.GetComponent<SlimeController>();
            if (player != null)
            {
                player.Die();
            }
            Destroy(gameObject); // �����ӵ�
        }
        // �����ײ�����Ƿ��ǵ��棨ͨ�� Layer �жϣ�
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject); // ��������ʱ�����ӵ�
        }
    }
}