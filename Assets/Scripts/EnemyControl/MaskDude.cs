using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskDude : MonoBehaviour
{
    [Header("�ٶ�����")]
    public float enemyRunSpeed;

    [Header("������")]
    public bool isGround;        // �Ƿ��ŵ�
    public bool _collided;

    [Header("��������")]
    public float attackRange = 10f; // ������Χ
    public float attackCooldown = 2.0f; // ������ȴʱ��
    public GameObject bulletPrefab; // �ӵ�Ԥ����
    private float lastAttackTime = 0f; // �ϴι���ʱ��

    [Header("���")]
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
        // �ƶ� MaskDude
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

        // �����⵽��ײ����ת MaskDude �ĳ���
        if (_collided)
        {
            Flip();
        }

        // ����Ƿ��ڵ�����
        FixedUpdateCheck();

        // �����Ҳ������ӵ�
        CheckForPlayerAndAttack();
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
        else if (collision.gameObject.tag == "Enemy")
        {
            // �ı��ƶ�����
            Flip();
        }
    }

    void FixedUpdateCheck()
    {
        // ����ɫ�Ƿ��ڵ���
        isGround = Physics2D.OverlapCircle(foot.position, 0.1f, groundLayer);
    }

    // ��ת MaskDude �ĳ���
    private void Flip()
    {
        // ��ת�ƶ�����
        enemyRunSpeed *= -1;
        // ��ת MaskDude �ĳ���
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // �����Ҳ������ӵ�
    private void CheckForPlayerAndAttack()
    {
        // �������Ƿ��ڹ�����Χ��
        Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D player in players)
        {
            if (player.CompareTag("Player") && Time.time > lastAttackTime + attackCooldown)
            {
                // �����ӵ�
                Shoot();

                // ��¼�ϴι���ʱ��
                lastAttackTime = Time.time;
                break; // ֻ����һ�����
            }
        }
    }

    // �����ӵ�
    private void Shoot()
    {
        if (bulletPrefab && rightUp && rightDown)
        {
            // ѡ���䷽������ʹ�� rightUp��
            Vector2 spawnPosition = rightUp.position;
            Vector2 direction = (rightUp.position - transform.position).normalized;

            // �����ӵ�
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

            // ���� MaskDude �ĳ�������ӵ�����ת
            if (transform.localScale.x > 0) // ��� MaskDude ����
            {
                bullet.transform.Rotate(0, -180, 0); // ��ת 180 ��
            }

            // �����ӵ�����
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript)
            {
                bulletScript.direction = direction;
            }
        }
    }

    // ���ƹ�����Χ
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}