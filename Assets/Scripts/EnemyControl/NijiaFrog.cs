using System;
using System.Collections;
using System.Collections.Generic;
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