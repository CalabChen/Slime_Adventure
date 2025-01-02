using UnityEngine;

public class ReachEnd : MonoBehaviour
{
    public Transform end; // 终点的Transform
    public float triggerDistance = 5.0f; // 触发的距离
    private Animator flagAnimator; // 终点的Animator组件

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