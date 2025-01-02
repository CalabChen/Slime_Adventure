using UnityEngine;

public class NijiaFrog : MonoBehaviour
{
    [Header("�ٶ�����")]
    public float enemyRunSpeed;

    [Header("������")]
    public bool isGround;        // �Ƿ��ŵ�
    public bool _collided;

    [Header("���")]
    public Transform foot;
    public LayerMask groundLayer;
    public BoxCollider2D headCollider; // ʹ�� BoxCollider2D ��Ϊ headPoint
    public Transform rightUp;
    public Transform rightDown;
    public Rigidbody2D enemyRB;
    public CapsuleCollider2D enemyCapColl;
    public Animator enemyAnim;

    private bool isDead = false; // ��־�����������ظ����������߼�

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
        // �ƶ� NijiaFrog
        Vector3 movement = new Vector3(enemyRunSpeed, enemyRB.velocity.y, 0);
        transform.position += movement * Time.deltaTime;

        // ���ǰ���Ƿ����ϰ���
        _collided = Physics2D.Linecast(rightUp.position, rightDown.position, groundLayer);

        // ���Ƶ�����
        if (_collided)
        {
            Debug.DrawLine(rightUp.position, rightDown.position, Color.red); // ��ײʱ��ʾ��ɫ��
        }
        else
        {
            Debug.DrawLine(rightUp.position, rightDown.position, Color.green); // δ��ײʱ��ʾ��ɫ��
        }

        // �����⵽��ײ����ת NijiaFrog �ĳ���
        if (_collided)
        {
            // ��ת NijiaFrog �ĳ���
            Flip();
        }

        // ����Ƿ��ڵ�����
        FixedUpdateCheck();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ��� Slime �Ƿ�ȵ� headPoint
        if (other.CompareTag("Player") && !isDead)
        {
            // ���� NijiaFrog �����߼�
            isDead = true; // ���ñ�־�����������ظ�����

            // �� Slime һ�����ϵ�����ȷ����������
            Rigidbody2D slimeRB = other.GetComponent<Rigidbody2D>();
            if (slimeRB != null)
            {
                slimeRB.velocity = new Vector2(slimeRB.velocity.x, 0); // ���� y ���ٶ�
                slimeRB.AddForce(Vector2.up * 12.0f, ForceMode2D.Impulse); // ��һ���ϴ�����ϵ���
            }

            // ������ײ�������� OnCollisionEnter2D ������
            enemyCapColl.enabled = false;

            // ���� NijiaFrog ���������������߼�
            enemyRunSpeed = 0f;
            enemyAnim.SetTrigger("die");
            Destroy(gameObject, 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ��� Slime �Ƿ���ײ�� NijiaFrog ��������λ
        if (collision.gameObject.CompareTag("Player") && !isDead)
        {
            // ���� Slime ��Ѫ�߼�
            SlimeController slime = collision.gameObject.GetComponent<SlimeController>();
            if (slime != null)
            {
                slime.Die(); // ���� Die ������������ Respawn
            }
        }
    }

    void FixedUpdateCheck()
    {
        // ����ɫ�Ƿ��ڵ���
        isGround = Physics2D.OverlapCircle(foot.position, 0.1f, groundLayer);
    }

    // ��ת NijiaFrog �ĳ���
    private void Flip()
    {
        // ��ת�ƶ�����
        enemyRunSpeed *= -1;
        // ��ת NijiaFrog �ĳ���
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}