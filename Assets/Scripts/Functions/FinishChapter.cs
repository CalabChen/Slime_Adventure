using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishChapter : MonoBehaviour
{
    public AudioSource finishSoundEffect; // ��Ч
    public GameObject portal; // Portal����
    private bool isTriggered = false; // ��ֹ�ظ�����

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true; // ����Ϊ�Ѵ���
            finishSoundEffect.Play(); // ������Ч
            portal.GetComponent<Animator>().Play("PortalOpen"); // ����PortalOpen����
            StartCoroutine(TeleportAfterAnimation(collision.gameObject)); // �ȴ�������������
        }
    }

    private IEnumerator TeleportAfterAnimation(GameObject player)
    {
        yield return new WaitForSeconds(1f);

        // ������ƶ���Portal��λ��
        player.transform.position = portal.transform.position;

        // �ȴ�һС��ʱ�䣨��ѡ��
        yield return new WaitForSeconds(2f);

        // ������һ��
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}