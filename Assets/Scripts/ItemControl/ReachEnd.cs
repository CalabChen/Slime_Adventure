using UnityEngine;

public class ReachEnd : MonoBehaviour
{
    public Transform end; // �յ��Transform
    public float triggerDistance = 5.0f; // �����ľ���
    private Animator flagAnimator; // �յ��Animator���

    private void Start()
    {
        flagAnimator = end.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Vector3.Distance(end.position, SlimeController.Instance.transform.position) < triggerDistance)
        {
            flagAnimator.SetTrigger("Reach");
        }
    }
}