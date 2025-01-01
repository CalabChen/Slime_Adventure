using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformMove : MonoBehaviour
{
    public float waitTime = 0.5f;//��ֵ�ȴ�ʱ��
    private float _waitTime;//�ȴ�ʱ��
    public float Speed = 1;//�ٶ�
    public Vector3 StartPoi;//��ʼλ��
    public Vector3 EndPoi;//��Ҫ�ƶ�Ŀ��λ��
    private bool IsStoE = true;//�Ƿ�����ʼ�㵽��ֹ���״̬

    private Transform playerTransform;
    public Transform platformParent;
    platformMove(Vector3 EndPoi)//���캯������ֵ�յ�
    {
        this.EndPoi = EndPoi;
    }
    // Start is called before the first frame update
    void Start()
    {
        _waitTime = waitTime;
        StartPoi = transform.position;//��ʼ����ʼλ��
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 StoE = (EndPoi - StartPoi).normalized;//��ʼ��ָ����ֹ��ĵ�λ��������

        if (_waitTime > 0)
        {
            _waitTime -= Time.deltaTime;
        }
        else
        {
            if (IsStoE)//����ʼ�㵽��ֹ���״̬
            {
                gameObject.transform.Translate(Speed * Time.deltaTime * StoE);
                if (Vector2.Dot((EndPoi - transform.position), StoE) < 0)//��������յ� ��˼н���180
                {
                    _waitTime = waitTime;//�ȴ�ʱ������
                    IsStoE = false;//�ı�״̬
                }

            }
            else
            {

                gameObject.transform.Translate(-Speed * Time.deltaTime * StoE);
                if (Vector2.Dot((StartPoi - transform.position), -StoE) < 0)//���������� ��˼н���180
                {

                    _waitTime = waitTime;//�ȴ�ʱ������
                    IsStoE = true;//�ı�״̬
                }


            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Slime")
        {
            // ����ɫ����Ϊ�м丸������Ӷ���
            playerTransform = collision.transform;
            playerTransform.SetParent(platformParent);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Slime")
        {
            // ȡ����ɫ���м丸����ĸ��ӹ�ϵ
            playerTransform.SetParent(null);
            // �������
            playerTransform = null;
        }
    }
}