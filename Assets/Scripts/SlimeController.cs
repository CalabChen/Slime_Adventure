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
        // ��ȡˮƽ�����ϵ����� (A = -1, D = 1)
        float moveInput = Input.GetAxisRaw("Horizontal");

        // �����µ�λ��
        Vector3 newPosition = transform.position + new Vector3(moveInput * moveSpeed * Time.deltaTime, 0, 0);

        // ����λ��
        transform.position = newPosition;
    }
}