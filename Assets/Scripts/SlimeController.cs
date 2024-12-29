using UnityEngine;

public class SlimeController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // 获取水平方向上的输入 (A = -1, D = 1)
        float moveInput = Input.GetAxisRaw("Horizontal");

        // 计算新的位置
        Vector3 newPosition = transform.position + new Vector3(moveInput * moveSpeed * Time.deltaTime, 0, 0);

        // 更新位置
        transform.position = newPosition;
    }
}