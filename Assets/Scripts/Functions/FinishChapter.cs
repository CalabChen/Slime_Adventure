using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishChapter : MonoBehaviour
{
    public AudioSource finishSoundEffect; // 音效
    public GameObject portal; // Portal物体
    private bool isTriggered = false; // 防止重复触发

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true; // 设置为已触发
            finishSoundEffect.Play(); // 播放音效
            portal.GetComponent<Animator>().Play("PortalOpen"); // 播放PortalOpen动画
            StartCoroutine(TeleportAfterAnimation(collision.gameObject)); // 等待动画结束后传送
        }
    }

    private IEnumerator TeleportAfterAnimation(GameObject player)
    {
        yield return new WaitForSeconds(1f);

        // 将玩家移动到Portal的位置
        player.transform.position = portal.transform.position;

        // 等待一小段时间（可选）
        yield return new WaitForSeconds(2f);

        // 加载下一关
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}