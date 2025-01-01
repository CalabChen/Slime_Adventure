using UnityEngine;

public class RaiseFlag : MonoBehaviour
{
    public Transform flag; // 旗帜的Transform
    public float triggerDistance = 5.0f; // 触发升旗的距离
    private Animator flagAnimator; // 旗帜的Animator组件

    private void Start()
    {
        flagAnimator = flag.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Vector3.Distance(flag.position, SlimeController.Instance.transform.position) < triggerDistance)
        {
            flagAnimator.SetTrigger("Raise");
            UpdateRespawnPoint(); // 更新重生点
        }
    }

    // 调用此方法来更新重生点位置
    public void UpdateRespawnPoint()
    {
        SlimeController.Instance.SetRespawnPoint(flag.position); // 使用公共方法更新重生点
    }
}