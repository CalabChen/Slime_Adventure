using UnityEngine;

public class RaiseFlag : MonoBehaviour
{
    public Transform flag; // 旗帜的Transform
    public float triggerDistance = 5.0f; // 触发升旗的距离
    private Animator flagAnimator; // 旗帜的Animator组件

    // 假设SlimeController有一个用于重生点的Transform
    public Transform respawnPoint;

    private void Start()
    {
        flagAnimator = flag.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Vector3.Distance(flag.position, SlimeController.Instance.transform.position) < triggerDistance)
        {
            flagAnimator.SetTrigger("Raise");
            UpdateRespawnPoint();// 假设Animator中有一个名为"RaiseFlag"的Trigger
        }
    }

    // 调用此方法来更新重生点位置
    public void UpdateRespawnPoint()
    {
        if (respawnPoint != null)
        {
            respawnPoint.position = flag.position;
        }
    }
}