using UnityEngine;

public class RaiseFlag : MonoBehaviour
{
    public Transform flag; // ���ĵ�Transform
    public float triggerDistance = 5.0f; // ��������ľ���
    private Animator flagAnimator; // ���ĵ�Animator���

    private void Start()
    {
        flagAnimator = flag.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Vector3.Distance(flag.position, SlimeController.Instance.transform.position) < triggerDistance)
        {
            flagAnimator.SetTrigger("Raise");
            UpdateRespawnPoint(); // ����������
        }
    }

    // ���ô˷���������������λ��
    public void UpdateRespawnPoint()
    {
        SlimeController.Instance.SetRespawnPoint(flag.position); // ʹ�ù�����������������
    }
}