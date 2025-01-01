using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawMove : MonoBehaviour
{
    public float waitTime = 0.5f;//定值等待时间
    private float _waitTime;//等待时间
    public float Speed = 1;//速度
    public Vector3 StartPoi;//起始位置
    public Vector3 EndPoi;//需要移动目标位置
    private bool IsStoE = true;//是否是起始点到终止点的状态

    private Transform playerTransform;
    void platformMove(Vector3 EndPoi)//构造函数，赋值终点
    {
        this.EndPoi = EndPoi;
    }
    // Start is called before the first frame update
    void Start()
    {
        _waitTime = waitTime;
        StartPoi = transform.position;//初始化起始位置
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 StoE = (EndPoi - StartPoi).normalized;//起始点指向终止点的单位方向向量

        if (_waitTime > 0)
        {
            _waitTime -= Time.deltaTime;
        }
        else
        {
            if (IsStoE)//是起始点到终止点的状态
            {
                gameObject.transform.Translate(Speed * Time.deltaTime * StoE);
                if (Vector2.Dot((EndPoi - transform.position), StoE) < 0)//如果到达终点 点乘夹角是180
                {
                    _waitTime = waitTime;//等待时间重置
                    IsStoE = false;//改变状态
                }

            }
            else
            {

                gameObject.transform.Translate(-Speed * Time.deltaTime * StoE);
                if (Vector2.Dot((StartPoi - transform.position), -StoE) < 0)//如果到达起点 点乘夹角是180
                {

                    _waitTime = waitTime;//等待时间重置
                    IsStoE = true;//改变状态
                }


            }
        }
    }
}